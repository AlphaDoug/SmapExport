namespace LuaExporter
{
    partial class Lua导出工具
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lua导出工具));
            this.ExportLuaFileBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ExportEffectBtn = new System.Windows.Forms.Button();
            this.ExportConfigBtn = new System.Windows.Forms.Button();
            this.ImportLuaBtn = new System.Windows.Forms.Button();
            this.ImportTabelBtn = new System.Windows.Forms.Button();
            this.ShowUseTipsBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExportLuaFileBtn
            // 
            this.ExportLuaFileBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExportLuaFileBtn.Location = new System.Drawing.Point(19, 108);
            this.ExportLuaFileBtn.Name = "ExportLuaFileBtn";
            this.ExportLuaFileBtn.Size = new System.Drawing.Size(192, 31);
            this.ExportLuaFileBtn.TabIndex = 0;
            this.ExportLuaFileBtn.Text = "从Smap中导出Lua";
            this.ExportLuaFileBtn.UseVisualStyleBackColor = true;
            this.ExportLuaFileBtn.Click += new System.EventHandler(this.ExportLuaFileBtnClick);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(146, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(425, 32);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(146, 56);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(425, 32);
            this.textBox2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "选择Smap文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(17, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "选择导出目录";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(577, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 31);
            this.button2.TabIndex = 4;
            this.button2.Text = "浏览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(577, 58);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 31);
            this.button3.TabIndex = 4;
            this.button3.Text = "浏览";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ExportEffectBtn
            // 
            this.ExportEffectBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExportEffectBtn.Location = new System.Drawing.Point(454, 108);
            this.ExportEffectBtn.Name = "ExportEffectBtn";
            this.ExportEffectBtn.Size = new System.Drawing.Size(197, 31);
            this.ExportEffectBtn.TabIndex = 5;
            this.ExportEffectBtn.Text = "导出特效";
            this.ExportEffectBtn.UseVisualStyleBackColor = true;
            this.ExportEffectBtn.Click += new System.EventHandler(this.ExportEffectBtn_Click);
            // 
            // ExportConfigBtn
            // 
            this.ExportConfigBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExportConfigBtn.Location = new System.Drawing.Point(235, 108);
            this.ExportConfigBtn.Name = "ExportConfigBtn";
            this.ExportConfigBtn.Size = new System.Drawing.Size(197, 31);
            this.ExportConfigBtn.TabIndex = 5;
            this.ExportConfigBtn.Text = "从Smap中导出配置";
            this.ExportConfigBtn.UseVisualStyleBackColor = true;
            this.ExportConfigBtn.Click += new System.EventHandler(this.ExportConfigBtn_Click);
            // 
            // ImportLuaBtn
            // 
            this.ImportLuaBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ImportLuaBtn.Location = new System.Drawing.Point(19, 156);
            this.ImportLuaBtn.Name = "ImportLuaBtn";
            this.ImportLuaBtn.Size = new System.Drawing.Size(192, 31);
            this.ImportLuaBtn.TabIndex = 0;
            this.ImportLuaBtn.Text = "将Lua导入Smap";
            this.ImportLuaBtn.UseVisualStyleBackColor = true;
            this.ImportLuaBtn.Click += new System.EventHandler(this.ImportLuaBtn_Click);
            // 
            // ImportTabelBtn
            // 
            this.ImportTabelBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ImportTabelBtn.Location = new System.Drawing.Point(235, 156);
            this.ImportTabelBtn.Name = "ImportTabelBtn";
            this.ImportTabelBtn.Size = new System.Drawing.Size(197, 31);
            this.ImportTabelBtn.TabIndex = 5;
            this.ImportTabelBtn.Text = "将配置导入Smap";
            this.ImportTabelBtn.UseVisualStyleBackColor = true;
            this.ImportTabelBtn.Click += new System.EventHandler(this.ImportTabelBtn_Click);
            // 
            // ShowUseTipsBtn
            // 
            this.ShowUseTipsBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ShowUseTipsBtn.Location = new System.Drawing.Point(454, 156);
            this.ShowUseTipsBtn.Name = "ShowUseTipsBtn";
            this.ShowUseTipsBtn.Size = new System.Drawing.Size(197, 31);
            this.ShowUseTipsBtn.TabIndex = 5;
            this.ShowUseTipsBtn.Text = "使用说明";
            this.ShowUseTipsBtn.UseVisualStyleBackColor = true;
            this.ShowUseTipsBtn.Click += new System.EventHandler(this.ShowUseTipsBtn_Click);
            // 
            // Lua导出工具
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 214);
            this.Controls.Add(this.ImportTabelBtn);
            this.Controls.Add(this.ExportConfigBtn);
            this.Controls.Add(this.ShowUseTipsBtn);
            this.Controls.Add(this.ExportEffectBtn);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ImportLuaBtn);
            this.Controls.Add(this.ExportLuaFileBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Lua导出工具";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smap导出工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Lua导出工具_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExportLuaFileBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button ExportEffectBtn;
        private System.Windows.Forms.Button ExportConfigBtn;
        private System.Windows.Forms.Button ImportLuaBtn;
        private System.Windows.Forms.Button ImportTabelBtn;
        private System.Windows.Forms.Button ShowUseTipsBtn;
    }
}

