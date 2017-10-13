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
    public partial class SetDoorInfo : Form
    {
        public NodeInfos m_nodeInfo = null;


        public SetDoorInfo(NodeInfos nodeInfo)
        {
            InitializeComponent();

            m_nodeInfo = nodeInfo;
        }
        private void SetDoorInfo_Load(object sender, EventArgs e)
        {
            foreach (var v in typeof(NodeType).GetFields())
            {
                if (v.FieldType.IsEnum == true)
                {
                    this.comboBoxMLX.Items.Add(v.Name);
                }
            }

            this.textBoxJDBM.Text = m_nodeInfo.Code;
            this.textBoxJDMC.Text = m_nodeInfo.Name;
            this.comboBoxMLX.Text = m_nodeInfo.Type.ToString();
            this.textBoxFPGL.Text = m_nodeInfo.Fpgl;
            this.richTextBoxGLMS.Text = m_nodeInfo.Glms;
            this.richTextBoxJDMS.Text = m_nodeInfo.Jdms;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_nodeInfo.Name = this.textBoxJDMC.Text;
            m_nodeInfo.Type = SetType(); 
            m_nodeInfo.Fpgl = this.textBoxFPGL.Text;
            m_nodeInfo.Glms = this.richTextBoxGLMS.Text;
            m_nodeInfo.Jdms = this.richTextBoxJDMS.Text;
        }
        private NodeType SetType()
        {
            if (this.comboBoxMLX.Text == "或门")
                return NodeType.或门;
            if (this.comboBoxMLX.Text == "与门")
                return NodeType.与门;
            if (this.comboBoxMLX.Text == "优先与门")
                return NodeType.优先与门;
            if (this.comboBoxMLX.Text == "禁止门")
                return NodeType.禁止门;
            return NodeType.或门;
        }
    }
}
