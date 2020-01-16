using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaExporter
{
    class ToolClass
    {
        /// <summary>
        /// 压缩json
        /// </summary>
        /// <param name="json">待压缩的json字符串</param>
        /// <returns>压缩后的json字符串</returns>
        public static string Compress(string json)
        {
            StringBuilder sb = new StringBuilder();
            using (StringReader reader = new StringReader(json))
            {
                int ch = -1;
                int lastch = -1;
                bool isQuoteStart = false;
                while ((ch = reader.Read()) > -1)
                {
                    if ((char)lastch != '\\' && (char)ch == '\"')
                    {
                        if (!isQuoteStart)
                        {
                            isQuoteStart = true;
                        }
                        else
                        {
                            isQuoteStart = false;
                        }
                    }
                    if (!Char.IsWhiteSpace((char)ch) || isQuoteStart)
                    {
                        sb.Append((char)ch);
                    }
                    lastch = ch;
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 格式化json
        /// </summary>
        /// <param name="json">待格式化的json字符串</param>
        /// <returns>格式化后的json字符串</returns>
        public static string Format(string json)
        {
            string strCompress = Compress(json);
            StringBuilder sb = new StringBuilder();
            #region format

            using (StringReader reader = new StringReader(strCompress))
            {
                using (StringWriter writer = new StringWriter(sb))
                {
                    int ch = -1;
                    int lastch = -1;
                    bool isQuoteStart = false;
                    while ((ch = reader.Read()) > -1)
                    {
                        StringBuilder temp = new StringBuilder();
                        switch ((char)ch)
                        {
                            case '{':
                                if (isQuoteStart)
                                {
                                    temp.Append('{');
                                }
                                else
                                {
                                    temp.Append('{');
                                    if ((char)reader.Peek() != '}')
                                    {
                                        temp.Append(Environment.NewLine);
                                    }
                                }
                                break;
                            case '}':
                                if (isQuoteStart)
                                {
                                    temp.Append('}');
                                }
                                else
                                {
                                    if ((char)lastch != '{' && (char)lastch != '}')
                                    {
                                        temp.Append(Environment.NewLine);
                                    }
                                    temp.Append('}');
                                    if ((char)reader.Peek() != ',')
                                    {
                                        temp.Append(Environment.NewLine);
                                    }
                                }
                                break;
                            case '[':
                                if (isQuoteStart)
                                {
                                    temp.Append('[');
                                }
                                else
                                {
                                    temp.Append('[');
                                    if ((char)reader.Peek() != ']')
                                    {
                                        temp.Append(Environment.NewLine);
                                    }
                                }
                                break;
                            case ']':
                                if (isQuoteStart)
                                {
                                    temp.Append(']');
                                }
                                else
                                {
                                    if ((char)lastch != '[' && (char)lastch != ']')
                                    {
                                        temp.Append(Environment.NewLine);
                                    }
                                    temp.Append(']');
                                    if ((char)reader.Peek() != ',' && (char)reader.Peek() != '}')
                                    {
                                        temp.Append(Environment.NewLine);
                                    }
                                }
                                break;
                            case '\"':
                                if ((char)lastch != '\\')
                                {
                                    if (!isQuoteStart)
                                    {
                                        isQuoteStart = true;
                                    }
                                    else
                                    {
                                        isQuoteStart = false;
                                    }
                                }
                                temp.Append("\"");
                                break;
                            case ':':
                                if (isQuoteStart)
                                {
                                    temp.Append(':');
                                }
                                else
                                {
                                    temp.Append(':');
                                    temp.Append(" ");
                                }
                                break;
                            case ',':
                                if (isQuoteStart)
                                {
                                    temp.Append(',');
                                }
                                else
                                {
                                    temp.Append(',');
                                    temp.Append(Environment.NewLine);
                                }
                                break;
                            case ' ':
                                if (isQuoteStart)
                                {
                                    temp.Append(" ");
                                }
                                else
                                {
                                    temp.Append("");
                                    temp.Append(Environment.NewLine);
                                }
                                break;
                            default:
                                temp.Append((char)ch);
                                break;
                        }
                        writer.Write(temp.ToString());
                        lastch = ch;
                    }
                }
            }
            #endregion format

            #region indent
            StringBuilder res = new StringBuilder();
            using (StringReader reader = new StringReader(sb.ToString()))
            {
                using (StringWriter writer = new StringWriter(res))
                {
                    string str = null;

                    int nspace = 0;
                    string space = "\t";
                    bool bEndMid = false;
                    while ((str = reader.ReadLine()) != null)
                    {
                        if (str.Length == 0) continue;
                        if (str.EndsWith("},"))
                        {
                            nspace--;
                        }
                        StringBuilder temp = new StringBuilder();
                        if (!bEndMid)
                        {
                            for (int i = 0; i < (str.EndsWith("],") || (str.EndsWith("}") && !str.EndsWith("{}")) || str.EndsWith("]") ? nspace - 1 : nspace); i++)
                            {
                                temp.Append(space);
                            }
                        }

                        temp.Append(str);
                        if (str.EndsWith("["))
                        {
                            writer.Write(temp);
                            bEndMid = true;
                        }
                        else
                        {
                            writer.WriteLine(temp);
                            bEndMid = false;
                        }
                        if (!(str.EndsWith("{}") || str.EndsWith("[]")))
                        {
                            if (str.StartsWith("{") || str.EndsWith("{") ||
                                str.EndsWith("["))
                            {
                                nspace++;
                            }
                            if (str.EndsWith("}") || str.EndsWith("]"))
                            {
                                nspace--;
                            }
                        }
                    }
                }
            }
            return res.ToString();
            #endregion indent
        }

        #region 直接删除指定目录下的所有文件及文件夹(保留目录)

        public static void DeleteDir(string file)
        {
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性
                System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(file))
                {
                    foreach (string f in Directory.GetFileSystemEntries(file))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                            Console.WriteLine(f);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDir(f);
                        }
                    }

                    //删除空文件夹
                    //Directory.Delete(file);
                    //Console.WriteLine(file);
                }

            }
            catch (Exception ex) // 异常处理
            {
                Console.WriteLine(ex.Message.ToString());// 异常信息
            }
        }

        #endregion
    }
}
