using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SyswareFlowChartTest
{
    public partial class MsgForm : Form
    {
        public MsgForm(string msg, bool showCancel = false,string title="提示")
        {
            InitializeComponent();
            this.MsgBox.Text = msg;
            this.MsgBox.TextAlign = HorizontalAlignment.Center;
            this.Text = title;
            this.button2.Visible = showCancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
