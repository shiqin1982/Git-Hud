using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyswareFlowChartTest.Tools;

namespace SyswareFlowChartTest
{
    public partial class SetBotNode : Form
    {
        public NodeInfos m_nodeInfo = null;

        public SetBotNode(NodeInfos nodeInfo)
        {
            InitializeComponent();
            this.textBoxFPGL.LostFocus += textBox_LostFocus;
            this.textBoxJSZQ.LostFocus += textBox_LostFocus;
            this.textBoxGZL.LostFocus += textBox_LostFocus;
            this.textBoxBLSJ.LostFocus += textBox_LostFocus;
            m_nodeInfo = nodeInfo;
        }

        void textBox_LostFocus(object sender, EventArgs e)
        {
            VerificationHelper.textBoxVer((TextBox)sender, button1);
        }
        private void SetBotNode_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.Add("基本");
            this.comboBox1.SelectedIndex = 0;
            this.comboBoxMLX.DataSource = System.Enum.GetNames(typeof(AffairType));
            this.comboBoxMLX.SelectedIndex = 0;
            this.textBoxJDBM.Text = m_nodeInfo.Code;
            this.textBoxJDMC.Text = m_nodeInfo.Name;
            this.comboBoxMLX.Text = m_nodeInfo.AffaType.ToString();
            this.textBoxJSZQ.Text = m_nodeInfo.Jszq;
            this.textBoxFPGL.Text = m_nodeInfo.Fpgl;
            this.richTextBoxGLMS.Text = m_nodeInfo.Glms;
            this.textBoxGZL.Text = m_nodeInfo.Gzl;
            this.richTextBoxJDMS.Text = m_nodeInfo.Jdms;
            this.comboBox1.Text = m_nodeInfo.Mxlj;
            this.textBoxBLSJ.Text = m_nodeInfo.exposureTime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxFPGL.BackColor == Macro.verColor || textBoxJSZQ.BackColor == Macro.verColor ||
                textBoxGZL.BackColor == Macro.verColor || textBoxBLSJ.BackColor == Macro.verColor)
            {
                MsgForm mf = new MsgForm("您输入有误，请检查输入内容！");
                mf.ShowDialog();
            }
            else
            {
                m_nodeInfo.Name = this.textBoxJDMC.Text;
                m_nodeInfo.AffaType = SetType();// this.comboBoxMLX.Text;
                m_nodeInfo.Jszq = this.textBoxJSZQ.Text;
                m_nodeInfo.Fpgl = this.textBoxFPGL.Text;
                m_nodeInfo.Glms = this.richTextBoxGLMS.Text;
                m_nodeInfo.Gzl = this.textBoxGZL.Text;
                m_nodeInfo.Jdms = this.richTextBoxJDMS.Text;
                m_nodeInfo.Mxlj = this.comboBox1.Text;
                m_nodeInfo.exposureTime = this.textBoxBLSJ.Text;
            }
        }
        private AffairType SetType()
        {
            if (this.comboBoxMLX.Text == "基本事件")
                return AffairType.基本事件;
            if (this.comboBoxMLX.Text == "条件事件")
                return AffairType.条件事件;
            if (this.comboBoxMLX.Text == "房型事件")
                return AffairType.房型事件;
            if (this.comboBoxMLX.Text == "未展开事件")
                return AffairType.未展开事件;
            if (this.comboBoxMLX.Text == "隐蔽事件")
                return AffairType.隐蔽事件;
            return AffairType.基本事件;
        }

        private void comboBoxMLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox1.Items.Clear();
            if (this.comboBoxMLX.Text == "房型事件")
            {
                //this.comboBox1.Items.Add("基本");
                this.comboBox1.Items.Add("True");
                this.comboBox1.Items.Add("False");
                this.comboBox1.SelectedIndex = 0;
            }
            else
            {
                this.comboBox1.Items.Add("基本");
                this.comboBox1.SelectedIndex = 0;
            }
        }
    }
}
