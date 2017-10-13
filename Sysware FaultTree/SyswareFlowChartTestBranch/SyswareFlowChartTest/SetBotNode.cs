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
    public partial class SetBotNode : Form
    {
        public NodeInfos m_nodeInfo = null;

        public SetBotNode(NodeInfos nodeInfo)
        {
            InitializeComponent();

            m_nodeInfo = nodeInfo;
        }
        private void SetBotNode_Load(object sender, EventArgs e)
        {
            this.comboBoxMLX.DataSource = System.Enum.GetNames(typeof(AffairType));
            this.textBoxJDBM.Text = m_nodeInfo.Code;
            this.textBoxJDMC.Text = m_nodeInfo.Name;
            this.comboBoxMLX.Text = m_nodeInfo.AffaType.ToString();
            this.textBoxJSZQ.Text = m_nodeInfo.Fxxs;
            this.textBoxFPGL.Text = m_nodeInfo.Fpgl;
            this.richTextBoxGLMS.Text = m_nodeInfo.Glms;
            this.textBoxGZL.Text = m_nodeInfo.Pjsxgl;
            this.richTextBoxJDMS.Text = m_nodeInfo.Jdms;
            this.comboBox1.Text = m_nodeInfo.Mxlj;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_nodeInfo.Name = this.textBoxJDMC.Text;
            m_nodeInfo.AffaType = SetType();// this.comboBoxMLX.Text;
            m_nodeInfo.Jszq = this.textBoxJSZQ.Text;
            m_nodeInfo.Fpgl = this.textBoxFPGL.Text;
            m_nodeInfo.Glms = this.richTextBoxGLMS.Text;
            m_nodeInfo.Gzl = this.textBoxGZL.Text;
            m_nodeInfo.Jdms = this.richTextBoxJDMS.Text;
            m_nodeInfo.Mxlj = this.comboBox1.Text;
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
                this.comboBox1.Items.Add("基本");
                this.comboBox1.Items.Add("True");
                this.comboBox1.Items.Add("False");
            }
            else this.comboBox1.Items.Add("基本");
        }
    }
}
