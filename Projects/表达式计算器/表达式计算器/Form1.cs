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
            char[] a = expression.ToCharArray(0, length);
            double[] data = new double[10];
            char[] oper = new char[10];
            int datacount = 0, opercount = 0;
            double temp = 0;
            int dicimal = -1;
            double tempdi = 1;
            for(int m=0;m<length;m++)
            {
                if(Char.IsDigit(a[m])==true)
                {
                    if(dicimal == -1)
                    {
                        temp*=10;
                        temp+=a[m]-'0';
                    }
                    else
                    {
                        dicimal++;
                        for(int q=0;q<dicimal;q++)
                            tempdi=tempdi*0.1;
                        temp=temp=(a[m]-'0')*tempdi;
                    }
                }
                else if(a[m]=='.')
                {
                    dicimal=0;
                }
                else
                {
                    dicimal=-1;
                    tempdi=1;
                    data[datacount]=temp;
                    datacount++;
                    temp=0;
                    oper[opercount]=a[m];
                    opercount++;
                }
            }
            data[datacount]=temp;
            datacount++;
            int temp1=0;
            int tempdata=0;
            while(temp1<opercount)
            {
                double para1=data[tempdata];
                tempdata++;
                double para2=data[tempdata];
                if(oper[temp1]=='+')
                {
                    data[tempdata]=para1+para2;
                }
                else if(oper[temp1]=='-')
                {
                    data[tempdata]=para1-para2;
                }
                else if(oper[temp1]=='*')
                {
                    data[tempdata]=para1*para2;
                }
                else if(oper[temp1]=='/')
                {
                    data[tempdata]=para1/para2;
                }
                temp1++;
            }
            return data[tempdata];
        }
    }
}
