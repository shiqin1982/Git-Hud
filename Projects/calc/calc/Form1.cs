using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double temp1 = -1;//记录第一个数字
        int pos = 0;     //存储计算方式
        public void addNum(int num)
        {
            textBox1.Text = textBox1.Text + num.ToString();
            //如果显示框
        }
        //7
        private void btnTest1_Click(object sender, EventArgs e)
        {
            addNum(7);//在显示屏中添加数字
        }
        //8
        private void btnTest2_Click(object sender, EventArgs e)
        {
            addNum(8);//在显示屏中添加数字
        }
        //9
        private void button3_Click(object sender, EventArgs e)
        {
            addNum(9);//在显示屏中添加数字
        }
        //4
        private void btnTest6_Click(object sender, EventArgs e)
        {
            addNum(4);//在显示屏中添加数字
        }
        //5
        private void btnTest5_Click(object sender, EventArgs e)
        {
            addNum(5);//在显示屏中添加数字
        }
        //6
        private void button4_Click(object sender, EventArgs e)
        {
            addNum(6);//在显示屏中添加数字
        }
        //1
        private void btnTest9_Click(object sender, EventArgs e)
        {
            addNum(1);//在显示屏中添加数字
        }
        //2
        private void button8_Click(object sender, EventArgs e)
        {
            addNum(2);//在显示屏中添加数字
        }
        //3
        private void button7_Click(object sender, EventArgs e)
        {
            addNum(3);//在显示屏中添加数字
        }
        //0
        private void btnTest11_Click(object sender, EventArgs e)
        {
            addNum(0);//在显示屏中添加数字
        }
        //除法
        private void button15_Click(object sender, EventArgs e)
        {
            pos = 4;//修改计算方式标志位
            temp1 = Convert.ToInt64(textBox1.Text);//获取前一个数值
            textBox1.Text = "";
        }
        //乘法
        private void button14_Click(object sender, EventArgs e)
        {
            pos = 3;//修改计算方式标志位
            temp1 = Convert.ToInt64(textBox1.Text);//获取前一个数值
            textBox1.Text = "";
        }
        //减法
        private void button13_Click(object sender, EventArgs e)
        {
            pos = 2;//修改计算方式标志位
            temp1 = Convert.ToInt64(textBox1.Text);//获取一个数值
            textBox1.Text = "";
        }
        //加法
        private void button12_Click(object sender, EventArgs e)
        {
            pos = 1;//修改计算方式标志位
            temp1 = Convert.ToDouble(textBox1.Text);//获取前一个数值
            textBox1.Text = "";
        }
        //等于
        private void button17_Click(object sender, EventArgs e)
        {
            double temp2 = Convert.ToDouble(textBox1.Text);//记录第二个数字
            switch (pos)//根据计算方式进行计算，显示计算结果
            {
                case 1:
                    textBox1.Text = (temp1 + temp2).ToString();
                    break;
                case 2:
                    textBox1.Text = (temp1 - temp2).ToString();
                    break;
                case 3:
                    textBox1.Text = (temp1 * temp2).ToString();
                    break;
                case 4:
                    textBox1.Text = (temp1 / temp2).ToString();
                    break;
            }
        }
        //归零
        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";//显示屏清空
            temp1 = 0;//临时计算结果归零
            pos = 0;//计算方式归零
        }
        ////小数点
        private void button10_Click(object sender, EventArgs e)
        {
            //如果直接点击小数点，则添加(0.)
            if (textBox1.Text == "")
                textBox1.Text = "0.";
            //只能添加一个小数点
            else if (textBox1.Text.IndexOf(".") >= 0)
                MessageBox.Show("已经添加了小数点！", "提示");
            //在显示框中最后一位添加小数点
            else
                textBox1.Text = textBox1.Text + ".";
        }
    }
}

