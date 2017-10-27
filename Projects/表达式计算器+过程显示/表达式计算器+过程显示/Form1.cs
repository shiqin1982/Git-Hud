using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 表达式计算器_过程显示
{
    public partial class Form1 : Form
    {
        public String expression;
        public String result = "";
        public class op
        {
            public char symbol;//符号
            public int grade;//优先级
            public int minus = 0;
            public op(char sym)
            {
                symbol = sym;
                if ((symbol == '+') || (symbol == '-'))
                    grade = 0;
                else if ((symbol == '*') || (symbol == '/'))
                    grade = 1;
                else if (symbol == '(')
                    grade = -2;
                else if (symbol == ')')
                    grade = -3;
                else if (symbol == '=')
                    grade = 0;
            }
            public double calculate(double p1, double p2)
            {
                if (symbol == '+')
                    return p1 + p2;
                else if (symbol == '-')
                    return p1 - p2;
                else if (symbol == '*')
                    return p1 * p2;
                else if (symbol == '/')
                    return p1 / p2;
                else
                    return p2;
            }
        }
        public class datanum
        {
            public double number;
            public bool isdeleted = false;
            public datanum(double num)
            {
                number = num;
            }
            public void delete()
            {
                isdeleted = true;
            }
        }
        public Form1()
        {
            InitializeComponent();
            textBox2.ReadOnly = true;
        }
        private void Form1_load(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            expression = textBox1.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Clear();
            result = result + expression + "=" + Result(expression) + "r\n";
            textBox2.Text = result;
            textBox1.Text = "" + Result(expression);
            textBox2.ScrollToCaret();
            this.textBox2.Focus();//获取焦点
            this.textBox2.Select(this.textBox2.TextLength - 1, 0);//光标定位到文本最后
            this.textBox2.ScrollToCaret();//滚动到光标处
            this.textBox1.Focus();
            this.textBox1.Select(this.textBox1.TextLength, 0);
        }
        private double Result(String expression)
        {
            int length = expression.Length;
            char[] a = expression.ToCharArray(0, length);
            datanum[] data = new datanum[100];
            op[] oper = new op[100];
            int datacount = 0, opercount = 0;
            double temp = 0;
            int dicimal = -1;
            double tempdi = 1;
            int brcount = 0;
            for(int m=0;m<length;m++)
            {
                if(Char.IsDigit(a[m])==true)
                {
                    if(dicimal==-1)
                    {
                        temp *= 10;
                        temp += a[m] - '0';
                    }
                    else
                    {
                        dicimal++;
                        for (int q = 0; q < dicimal; q++)
                            tempdi = tempdi * 0.1;
                        temp = temp + (a[m] - '0') * tempdi;
                        tempdi = 1;
                    }
                }
                else if(a[m]=='.')
                {
                    dicimal = 0;
                }
                else if(a[m]==')')
                {
                    int q = m;
                    brcount = 0;
                    for(;q>0;q--)
                    {
                        if (a[q] == ')')
                            break;
                        else
                            brcount++;
                    }
                    for(int z=0;z<brcount;z++)
                    {
                        oper[opercount - z].grade += 2;
                    }
                }
                else if(a[m]=='(')
                {
                }
                else
                {
                    dicimal = -1;
                    tempdi = 1;
                    data[datacount] = new datanum(temp);
                    datacount++;
                    temp = 0;
                    oper[opercount] = new op(a[m]);
                    opercount++;
                }
            }
            data[datacount] = new datanum(temp);//最后一个数
            datacount++;

            /* int temp1=0;
               int tempdata=0;
               while(temp1<opercount)
               {
                    double para1=data[tempdata].number;
                    tempdata++;
                    double para2=data[tempdata].number;
                    if(oper[temp1].symbol=='+')
                    {
                        data[temp1].number = para1 + para2;
                    }
                    else if(oper[temp1].symbol=='-')
                    {
                        data[tempdata].number = para1 - para2;
                    }
                    else if(oper[temp1].symbol=='*')
                    {
                        data[tempdata].number = para1 * para2;
                    }
                    else if(oper[temp1].symbol=='/')
                    {
                        data[tempdata].number = para1 / para2;
                    }
                    temp1++;
                }
                return data[tempdata].number;*/
            
            double n=0;
            int temp1;
            int tempgrade=1;
            int[]lossdata=new int[10];
            int pa=0;
            int pb=0;
            while(tempgrade>=-1)
            {
                for(temp1=0;temp1<opercount;temp1++)
                {
                    if(oper[temp1].grade==tempgrade)
                    {
                        pa=temp1;
                        pb=temp1+1;
                        while(data[pa].isdeleted==true)
                        {
                            pa--;
                        }
                        while(data[pa].isdeleted==true)
                        {
                            pb++;
                        }
                        data[pa].number=oper[temp1].calculate(data[pa].number,data[pb].number);
                        n=data[pa].number;
                        data[pb].delete();
                    }
                }
                tempgrade--;
            }
            return n;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
