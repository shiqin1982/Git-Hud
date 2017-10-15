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
    public partial class SetTopNode : Form
    {
        public NodeInfos m_nodeInfo = null;

        public SetTopNode(NodeInfos nodeInfo)
        {
            InitializeComponent();

            m_nodeInfo = nodeInfo;
        }
        private void SetTopNode_Load(object sender, EventArgs e)
        {
            //foreach (var v in typeof(NodeType).GetFields())
            //{
            //    if (v.FieldType.IsEnum == true)
            //    {
            //        this.comboBoxMLX.Items.Add(v.Name);
            //    }
            //}
            this.comboBoxMLX.DataSource = System.Enum.GetNames(typeof(NodeType));              
            this.textBoxJDBM.Text = m_nodeInfo.Code;
            this.textBoxJDMC.Text = m_nodeInfo.Name;
            this.comboBoxMLX.Text = m_nodeInfo.Type.ToString();
            this.textBoxFXXS.Text = m_nodeInfo.Fxxs;
            this.textBoxFPGL.Text = m_nodeInfo.Fpgl;
            this.richTextBoxGLMS.Text = m_nodeInfo.Glms;
            this.textBoxPJSXGL.Text = m_nodeInfo.Pjsxgl;
            this.richTextBoxJDMS.Text = m_nodeInfo.Jdms;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_nodeInfo.Name = this.textBoxJDMC.Text;
            m_nodeInfo.Type = SetType();
            m_nodeInfo.Fxxs = this.textBoxFXXS.Text;
            m_nodeInfo.Fpgl = this.textBoxFPGL.Text;
            m_nodeInfo.Glms = this.richTextBoxGLMS.Text;
            m_nodeInfo.Pjsxgl = this.textBoxPJSXGL.Text;
            m_nodeInfo.Jdms = this.richTextBoxJDMS.Text;
            //this.comboBoxMLX.Text = m_nodeInfo.Type.ToString();
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
