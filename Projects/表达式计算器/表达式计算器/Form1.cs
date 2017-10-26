using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 表达式计算器
{
    public partial class Form1 : Form
    {
        public String expression;
        public Form1()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender,EventArgs e)
        {
            expression = textBox1.Text;
        }
        private void button1_Click(object sender,EventArgs e)
        {
            textBox1.Text = expression + "=" + Result(expression);
        }
        private double Result(String expression)
        {
            int length = expression.Length;
        }
    }
}
