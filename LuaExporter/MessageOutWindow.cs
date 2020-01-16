using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuaExporter
{
    public partial class MessageOutWindow : Form
    {
        public MessageOutWindow(string msg)
        {
            InitializeComponent();
            MessageTextBox.Text = msg;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
