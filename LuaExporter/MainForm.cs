using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;

namespace LuaExporter
{
    public partial class Lua导出工具 : Form
    {
        private string tatolData;
        private static JObject jObject;
        private string luaText;
        private string luaFileName;
        private string tabelFileText;
        private string tabelFileName;
        private RegistryKey software = Registry.CurrentUser.CreateSubKey("SOFTWARE\\LuaExporter");

        private string smapFilePath = "";
        private string exportDir = "";

        private string smapFileName = "";
        public Lua导出工具()
        {
            InitializeComponent();
            if (software.GetValueNames().Length == 0)
            {
                software.SetValue("SmapFilePath", textBox1.Text);
                software.SetValue("exportDir", textBox2.Text);
            }
            else
            {
                for (int i = 0; i < software.GetValueNames().Length; i++)
                {
                    if (software.GetValueNames()[i] == "SmapFilePath")
                    {
                        textBox1.Text = software.GetValue("SmapFilePath").ToString();
                    }
                    if (software.GetValueNames()[i] == "exportDir")
                    {
                        textBox2.Text = software.GetValue("exportDir").ToString();
                    }
                }
            }
        }
       
        /// <summary>
        /// 选择Smap文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择smap文件";
            dialog.Filter = "Smap文件(*.smap)|*.smap";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                software.SetValue("SmapFilePath", textBox1.Text);
            }
        }
        /// <summary>
        /// 选择lua文件的导出目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "请选择导出路径";
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderDialog.SelectedPath;
                software.SetValue("exportDir", textBox2.Text);
            }          
        }
        /// 浏览文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void ExploreFile(string filePath)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "explorer";
            //打开资源管理器
            proc.StartInfo.Arguments = @"/open," + filePath;
            //选中"notepad.exe"这个程序,即记事本
            proc.Start();
        }
        /// <summary>
        /// 导出特效按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportEffectBtn_Click(object sender, EventArgs e)
        {
            Button senderBtn = sender as Button;
            string effectName = "";
            string effectNames = "";
            senderBtn.Enabled = false;
            if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("请选择正确的Smap文件!");
                senderBtn.Enabled = true;
                return;
            }
            smapFilePath = textBox1.Text;
            FileStream stream = new FileStream(smapFilePath, FileMode.Open);//fileMode指定是读取还是写入
            StreamReader reader = new StreamReader(stream);
            tatolData = reader.ReadToEnd();  //一次性读取全部数据
            reader.Close();
            stream.Close();
            smapFileName = Path.GetFileName(smapFilePath);
            jObject = JObject.Parse(tatolData);
            foreach (JToken item in jObject["mapdata"]["ObjectsData"])
            {
                if (item["class"].ToString() == "cOtherParticleObject")
                {
                    foreach (JToken item1 in item["components"])
                    {
                        if (item1["class"].ToString() == "sOtherParticleComponent")
                        {
                            effectName = item1["data"]["m_particleNameInput"].ToString();
                            Console.WriteLine(effectName);
                            effectNames = effectNames + effectName + Environment.NewLine;
                        }
                    }
                }
            }
            MessageOutWindow messageOutWindow = new MessageOutWindow(effectNames);
            messageOutWindow.ShowDialog();

            senderBtn.Enabled = true;
        }
        /// <summary>
        /// 导出配置按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportConfigBtn_Click(object sender, EventArgs e)
        {
            int m_rowNum = 0;
            int m_columnNum = 0;
            string[,] tabelData = null;
            Button senderBtn = sender as Button;
            ExportConfigBtn.Enabled = false;
            CheckPath(senderBtn);
            smapFilePath = textBox1.Text;
            exportDir = textBox2.Text;
            FileStream stream = new FileStream(smapFilePath, FileMode.Open);//fileMode指定是读取还是写入
            StreamReader reader = new StreamReader(stream);
            tatolData = reader.ReadToEnd();  //一次性读取全部数据
            reader.Close();
            stream.Close();
            smapFileName = Path.GetFileName(smapFilePath);
            jObject = JObject.Parse(tatolData);
            //在选择的目录下创建新的目录
            exportDir = exportDir + @"\" + "Tabels";
            Directory.CreateDirectory(exportDir);
            //将json中的表格配置数据读取进入一个二维数组
            foreach (JToken item in jObject["mapdata"]["ObjectsData"])
            {
                if (item["class"].ToString() == "cTableObject")
                {
                    tabelFileName = item["name"].ToString() + ".xlsx";
                    foreach (var item1 in item["components"])
                    {
                        if (item1["class"].ToString() == "sTableComponent")
                        {
                            m_rowNum = int.Parse(item1["data"]["m_dataModel"]["m_rowNum"].ToString());
                            m_columnNum = int.Parse(item1["data"]["m_dataModel"]["m_columnNum"].ToString());
                            tabelData = new string[m_rowNum, m_columnNum];//此表格中的数据存在一个这样的二维数组中
                            for (int i = 0; i < m_rowNum; i++)
                            {
                                for (int j = 0; j < m_columnNum; j++)
                                {
                                    tabelData[i, j] = item1["data"]["m_dataModel"]["m_tableData"][i][j].ToString();
                                }
                            }
                        }
                    }
                    string excelFileName = exportDir + @"\" + tabelFileName; 
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    //创建工作表
                    var sheet = workbook.CreateSheet(item["name"].ToString());
                    for (int i = 0; i < m_rowNum; i++)
                    {
                        var row = sheet.CreateRow(i);
                    }
                    for (int i = 0; i < m_rowNum; i++)
                    {
                        for (int j = 0; j < m_columnNum; j++)
                        {
                            var row = sheet.GetRow(i);
                            var cell = row.CreateCell(j);
                            cell.SetCellValue(tabelData[i, j]);
                        }
                    }
                    FileStream file = new FileStream(excelFileName, FileMode.Create);
                    workbook.Write(file);
                    file.Dispose();
                }
                
            }

            MessageBox.Show("配置文件导出成功!");
            ExploreFile(exportDir);
            senderBtn.Enabled = true;
        }

        private void Lua导出工具_FormClosed(object sender, FormClosedEventArgs e)
        {
            software.Close();
        }
        /// <summary>
        /// 导入Lua代码到smap文件中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportLuaBtn_Click(object sender, EventArgs e)
        {
            Button senderBtn = sender as Button;
            senderBtn.Enabled = false;
            CheckPath(senderBtn);
            smapFilePath = textBox1.Text;
            exportDir = textBox2.Text;
            FileStream stream = new FileStream(smapFilePath, FileMode.Open);//fileMode指定是读取还是写入
            StreamReader reader = new StreamReader(stream);
            tatolData = reader.ReadToEnd();  //一次性读取全部数据
            reader.Close();
            stream.Close();
            smapFileName = Path.GetFileName(smapFilePath);
            jObject = JObject.Parse(tatolData);
            exportDir = exportDir + @"\" + "Luas";
            if (!Directory.Exists(exportDir))
            {
                MessageBox.Show("请选择正确的导出目录!");
                return;
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(exportDir);
            FileInfo[] files = directoryInfo.GetFiles();
            string fileGuid = "";
            string jsonGuid = "";
            string luaName = "";
            FileStream fileStream = null;
            StreamReader streamReader = null;
            for (int i = 0; i < files.Length; i++)
            {
                fileStream = new FileStream(files[i].FullName, FileMode.Open);
                streamReader = new StreamReader(fileStream);
                luaText = streamReader.ReadToEnd();
                streamReader.Close();
                fileStream.Close();
                //fileGuid = files[i].Name.Split('[')[1].Replace("].lua", "");
                luaName = files[i].Name.Replace(".lua", "");
                foreach (JObject item in jObject["mapdata"]["ObjectsData"])
                {
                    foreach (JValue item1 in item["guid"])
                    {
                        jsonGuid = jsonGuid + item1.ToString() + "-";
                    }
                    if (item["class"].ToString() == "cScriptObject" ||
                        item["class"].ToString() == "cModuleScriptObject" ||
                        item["class"].ToString() == "cCustomEventObject" ||
                        item["class"].ToString() == "cCollisionEventObject" ||
                        item["class"].ToString() == "cStartFunctionObject" ||
                        item["class"].ToString() == "cTickFunctionObject" ||
                        item["class"].ToString() == "cCustomFunctionObject"
                        )
                    {
                        jsonGuid = jsonGuid.Remove(jsonGuid.Length - 1, 1);
                        string n = item["name"].ToString();
                        if (string.Equals(n, luaName))
                        {
                            foreach (JObject item1 in item["components"])
                            {
                                if (item1["class"].ToString() == "sLuaComponent")
                                {
                                    item1["data"]["m_luaContent"] = luaText;//测试通过后改成luaText
                                }
                            }
                        }
                    }                   
                    jsonGuid = "";
                }               
            }          
            FileStream fileStream1 = new FileStream(smapFilePath, FileMode.Create);
            StreamWriter streamWriter1 = new StreamWriter(fileStream1);
            streamWriter1.Write(ToolClass.Compress(jObject.ToString()));
            streamWriter1.Close();
            fileStream1.Close();
            jObject = null;
            senderBtn.Enabled = true;
        }
        /// <summary>
        /// 导入表格配置文件到smap文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportTabelBtn_Click(object sender, EventArgs e)
        {
            Button senderBtn = sender as Button;
            senderBtn.Enabled = false;
            CheckPath(senderBtn);
            smapFilePath = textBox1.Text;
            exportDir = textBox2.Text;
            FileStream stream = new FileStream(smapFilePath, FileMode.Open);//fileMode指定是读取还是写入
            StreamReader reader = new StreamReader(stream);
            tatolData = reader.ReadToEnd();  //一次性读取全部smap中的json到内存中
            reader.Close();
            stream.Close();
            smapFileName = Path.GetFileName(smapFilePath);
            jObject = JObject.Parse(tatolData);//将字符串转化为json格式
            exportDir = exportDir + @"\" + "Tabels";
            if (!Directory.Exists(exportDir))
            {
                MessageBox.Show("请选择正确的导出目录!");
                return;
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(exportDir);
            FileInfo[] files = directoryInfo.GetFiles();
            string fileGuid = "";
            string jsonGuid = "";
            string fileTabelName = "";
            string jsonTabelName = "";
            FileStream fileStream = null;
            HSSFWorkbook hSSFWorkbook = null;
            string[,] fileTabelData = null;//从xlsx文件中读取的表格数据

            for (int i = 0; i < files.Length; i++)
            {
                fileTabelName = files[i].Name.Replace(".xlsx", "");
                fileStream = new FileStream(files[i].FullName, FileMode.Open);
                hSSFWorkbook = new HSSFWorkbook(fileStream);
                var hSSFSheet = hSSFWorkbook.GetSheet(fileTabelName);
                fileStream.Close();
                //fileGuid = files[i].Name.Split('[')[1].Replace("].lua", "");
                //tabelName = files[i].Name.Split('[')[0];
                int currentRowIndex = 2;
                int currentColumnIndex = 1;
                int totalRowNum = 2;//配置表中的数据总行数,规定至少为2
                int totalColumnNum = 1;//配置表中的数据总列数,规定至少为1

                #region 检查配置表中的有效数据总行数
                while (true)
                {
                    if (hSSFSheet.GetRow(currentRowIndex) != null)
                    {
                        hSSFSheet.GetRow(currentRowIndex).GetCell(0).SetCellType(NPOI.SS.UserModel.CellType.String);
                        if (hSSFSheet.GetRow(currentRowIndex).GetCell(0).StringCellValue != "")
                        {
                            currentRowIndex = currentRowIndex + 1;
                            totalRowNum = currentRowIndex;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }                    
                }
                #endregion

                #region 检查配置表中的有效数据总列数
                hSSFSheet.GetRow(0).GetCell(currentColumnIndex).SetCellType(NPOI.SS.UserModel.CellType.String);
                string title = hSSFSheet.GetRow(0).GetCell(currentColumnIndex).StringCellValue;
                while (title == "String" ||
                       title == "Int" ||
                       title == "Boolean" ||
                       title == "Float" ||
                       title == "Vector2" ||
                       title == "Vector3" ||
                       title == "Euler" ||
                       title == "Color" 
                    )
                {
                    currentColumnIndex += 1;
                    if (hSSFSheet.GetRow(0).GetCell(currentColumnIndex) != null)
                    {
                        hSSFSheet.GetRow(0).GetCell(currentColumnIndex).SetCellType(NPOI.SS.UserModel.CellType.String);
                        title = hSSFSheet.GetRow(0).GetCell(currentColumnIndex).StringCellValue;
                        totalColumnNum = currentColumnIndex;
                    }
                    else
                    {
                        totalColumnNum = currentColumnIndex;
                        break;
                    }
                }


                #endregion      
                //Console.WriteLine("文件" + fileTabelName + "的行数为" + totalRowNum + "列数为" + totalColumnNum);
                fileTabelData = new string[totalRowNum, totalColumnNum];
                for (int j = 0; j < totalRowNum; j++)
                {
                    var row = hSSFSheet.GetRow(j);
                    for (int k = 0; k < totalColumnNum; k++)
                    {
                        var cell = row.GetCell(k);
                        if (cell == null)
                        {
                            fileTabelData[j, k] = string.Empty;
                        }
                        else
                        {
                            cell.SetCellType(NPOI.SS.UserModel.CellType.String);
                            fileTabelData[j, k] = cell.StringCellValue;
                        }                      
                    }
                }
                JArray[] tabelJArray = new JArray[totalRowNum];
                for (int j = 0; j < totalRowNum; j++)
                {
                    string[] rowString = new string[totalColumnNum];
                    for (int k = 0; k < totalColumnNum; k++)
                    {
                        rowString[k] = fileTabelData[j, k];
                    }
                    var rowJArray = new JArray(rowString);
                    tabelJArray[j] = new JArray(rowJArray);
                }
                JArray jArray = new JArray(tabelJArray);
                //Console.WriteLine(jArray.ToString());
                
                foreach (JObject item in jObject["mapdata"]["ObjectsData"])
                {
                    if (item["class"].ToString() == "cTableObject")
                    {
                        jsonTabelName = item["name"].ToString();
                        if (string.Equals(fileTabelName, jsonTabelName))
                        {
                            foreach (JObject item1 in item["components"])
                            {
                                if (item1["class"].ToString() == "sTableComponent")
                                {
                                    item1["data"]["m_dataModel"]["m_tableData"] = jArray;
                                }
                            }
                        }
                    }
                    //foreach (JValue item1 in item["guid"])
                    //{
                    //    jsonGuid = jsonGuid + item1.ToString() + "-";
                    //}
                    //jsonGuid = jsonGuid.Remove(jsonGuid.Length - 1, 1);

                    //jsonGuid = "";
                }
                
            }

            
            FileStream fileStream1 = new FileStream(smapFilePath, FileMode.Create);
            StreamWriter streamWriter1 = new StreamWriter(fileStream1);
            streamWriter1.Write(ToolClass.Compress(jObject.ToString()));
            streamWriter1.Close();
            fileStream1.Close();
            jObject = null;
            senderBtn.Enabled = true;
        }
        /// <summary>
        /// 检查用户选择的smap文件和路径是否正确
        /// </summary>
        /// <returns></returns>
        private bool CheckPath(Button btn)
        {
            if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("请选择正确的Smap文件!");
                btn.Enabled = true;
                return false;
            }
            if (!Directory.Exists(textBox2.Text))
            {
                MessageBox.Show("请选择正确的导出目录!");
                btn.Enabled = true;
                return false;
            }
            if (textBox2.Text.Substring(1) == ":\\")
            {
                MessageBox.Show("不能选择磁盘根目录.");
                btn.Enabled = true;
                return false;
            }
            return true;
        }

        private void ExportLuaFileBtnClick(object sender, EventArgs e)
        {
            Button senderBtn = sender as Button;
            ExportLuaFileBtn.Enabled = false;
            CheckPath(senderBtn);
            smapFilePath = textBox1.Text;
            exportDir = textBox2.Text;
            FileStream stream = new FileStream(smapFilePath, FileMode.Open);//fileMode指定是读取还是写入
            StreamReader reader = new StreamReader(stream);
            tatolData = reader.ReadToEnd();  //一次性读取全部数据
            reader.Close();
            stream.Close();
            smapFileName = Path.GetFileName(smapFilePath);
            jObject = JObject.Parse(tatolData);
            exportDir = exportDir + @"\" + "Luas";
            Directory.CreateDirectory(exportDir);
            //DeleteDir(exportDir);

            foreach (JToken item in jObject["mapdata"]["ObjectsData"])
            {
                if (item["class"].ToString() == "cScriptObject" ||
                    item["class"].ToString() == "cModuleScriptObject" ||
                    item["class"].ToString() == "cCustomEventObject" ||
                    item["class"].ToString() == "cCollisionEventObject" ||
                    item["class"].ToString() == "cStartFunctionObject" ||
                    item["class"].ToString() == "cTickFunctionObject" ||
                    item["class"].ToString() == "cCustomFunctionObject"
                    )
                {
                    Console.WriteLine(item["name"] + "是一个脚本文件,类型为" + item["class"]);
                    foreach (JToken item1 in item["components"])
                    {
                        if (item1["class"].ToString() == "sLuaComponent")
                        {
                            luaText = item1["data"]["m_luaContent"].ToString();
                            //Console.WriteLine("lua内容为" + luaText);
                            string guid = "[";
                            foreach (JToken _guid in item["guid"])
                            {
                                guid = guid + _guid.ToString() + "-";
                            }
                            guid = guid.Remove(guid.Length - 1, 1);
                            guid += "]";
                            string dir = exportDir + @"\" + item["name"].ToString() + @".lua";
                            //string newDir = dir;
                            //int i = 1;
                            //while (File.Exists(newDir))
                            //{
                            //    newDir = dir.Replace(@".lua", "") + i + @".lua";
                            //    i++;
                            //}

                            FileStream stream1 = new FileStream(dir, FileMode.Create);//fileMode指定是读取还是写入
                            StreamWriter writer1 = new StreamWriter(stream1);
                            writer1.Write(luaText);
                            writer1.Close();
                            stream.Close();
                        }
                    }
                }
            }
            MessageBox.Show("Lua文件导出成功!");
            ExploreFile(exportDir);
            senderBtn.Enabled = true;
        }

        private void ShowUseTipsBtn_Click(object sender, EventArgs e)
        {
            InstructionForm instructionForm = new InstructionForm();
            instructionForm.ShowDialog();
        }
    }
}