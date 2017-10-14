using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AfhaForm
{
    public partial class Form1 : Form
    {
        private string m_FilePath = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (m_FilePath == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xml文件 (*.xml)|*.xml";
                sfd.OverwritePrompt = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                    m_FilePath = sfd.FileName;
                else
                    return;
            }
            XDocument xdoc = new XDocument();
            XElement Root = new XElement("Root");
            xdoc.Add(Root);
            //foreach (DataGridViewRow row in this.dataGridView1.Rows)
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                Root.Add(GetRow(row));
            }
            xdoc.Save(m_FilePath);
        }
        private XElement GetRow(DataGridViewRow row)
        {
            XElement elm = new XElement("Item");
            elm.SetAttributeValue("编号", row.Cells[0].Value == null ? "" : row.Cells[0].Value);
            elm.SetAttributeValue("功能", row.Cells[1].Value == null ? "" : row.Cells[1].Value);
            elm.SetAttributeValue("失效状态", row.Cells[2].Value == null ? "" : row.Cells[2].Value);
            elm.SetAttributeValue("飞行阶段", row.Cells[3].Value == null ? "" : row.Cells[3].Value);
            elm.SetAttributeValue("失效影响", row.Cells[4].Value == null ? "" : row.Cells[4].Value);
            elm.SetAttributeValue("影响等级", row.Cells[5].Value == null ? "" : row.Cells[5].Value);
            elm.SetAttributeValue("安全性目标", row.Cells[6].Value == null ? "" : row.Cells[6].Value);
            elm.SetAttributeValue("设计目标", row.Cells[7].Value == null ? "" : row.Cells[7].Value);
            elm.SetAttributeValue("验证方法", row.Cells[8].Value == null ? "" : row.Cells[8].Value);
            elm.SetAttributeValue("证明材料", row.Cells[9].Value == null ? "" : row.Cells[9].Value);
            elm.SetAttributeValue("备注", row.Cells[10].Value == null ? "" : row.Cells[10].Value);
            return elm;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml文件 (*.xml)|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
                m_FilePath = ofd.FileName;
            else
                return;
            LoadFile(m_FilePath);
        }
        private void LoadFile(string file)
        {
            XDocument xdoc = XDocument.Load(file);
            XElement root = xdoc.Element("Root");
            this.dataGridView1.Rows.Clear();
            foreach (XElement elm in root.Elements("Item"))
            {
                AddRow(elm);
            }
        }
        private void AddRow(XElement elm)
        {
            int index = this.dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = elm.Attribute("编号").Value;
            dataGridView1.Rows[index].Cells[1].Value = elm.Attribute("功能").Value;
            dataGridView1.Rows[index].Cells[2].Value = elm.Attribute("失效状态").Value;
            dataGridView1.Rows[index].Cells[3].Value = elm.Attribute("飞行阶段").Value;
            dataGridView1.Rows[index].Cells[4].Value = elm.Attribute("失效影响").Value;
            dataGridView1.Rows[index].Cells[5].Value = elm.Attribute("影响等级").Value;
            dataGridView1.Rows[index].Cells[6].Value = elm.Attribute("安全性目标").Value;
            dataGridView1.Rows[index].Cells[7].Value = elm.Attribute("设计目标").Value;
            dataGridView1.Rows[index].Cells[8].Value = elm.Attribute("验证方法").Value;
            dataGridView1.Rows[index].Cells[9].Value = elm.Attribute("证明材料").Value;
            dataGridView1.Rows[index].Cells[10].Value = elm.Attribute("备注").Value;
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            string progname = "SyswareFlowChartTest.exe";
            string str = Process.GetCurrentProcess().MainModule.FileName;
            string filename = System.IO.Path.GetDirectoryName(str) + "\\" + progname;
            if (System.IO.File.Exists(filename) == false)
                return;
            Process myProcess = new Process();
            //filename = "notepad.exe";
            myProcess.StartInfo.FileName = filename;
            //myProcess.StartInfo.Verb = "Print";
            //myProcess.StartInfo.CreateNoWindow = true;
            myProcess.EnableRaisingEvents = true;
            myProcess.Exited += myProcess_Exited;
            this.Hide();
            // this.ShowInTaskbar = false;
            myProcess.Start();
        }

        void myProcess_Exited(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //this.ShowInTaskbar = true;
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string str = Process.GetCurrentProcess().MainModule.FileName;
            string filename = System.IO.Path.GetDirectoryName(str) + "\\pssa.xml";
            if (System.IO.File.Exists(filename))
                LoadFile(filename);
        }
    }
}
