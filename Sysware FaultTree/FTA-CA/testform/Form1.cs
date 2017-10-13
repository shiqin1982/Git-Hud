using FT2Cutset;
using FT2GenieBN;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BNWindowsInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*NEntrance go;*/
        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = (new CutsetEntrance()).GO(textBox4.Text); //割集计算结果
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            FTBNEntrance go = new FTBNEntrance(textBox4.Text, true);
            go.GOCalculateProbability(); //计算顶事件发生概率
            textBox3.Text = "顶事件发生概率：" +go.GetPrbResult();

            textBox3.Text += Environment.NewLine;
            textBox3.Text += Environment.NewLine;
            
            go.GOCalculateImportance(); // 计算底事件重要度
 
            textBox3.Text += Environment.NewLine + "重要度计算结果：";
            textBox3.Text += Environment.NewLine;
            textBox3.Text += go.GetIMPsResultXML();
        }
    }
}
