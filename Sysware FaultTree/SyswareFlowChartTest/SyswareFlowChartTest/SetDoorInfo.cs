using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MindFusion.Diagramming;

namespace SyswareFlowChartTest
{
    public partial class SetDoorInfo : Form
    {
        public NodeInfos m_nodeInfo = null;
        private Diagram mDiagram;

        public SetDoorInfo(NodeInfos nodeInfo, Diagram diagram)
        {
            InitializeComponent();

            m_nodeInfo = nodeInfo;
            mDiagram = diagram;
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
            this.checkBoxFY.Checked = m_nodeInfo.isPager;

            LoadSubNode();
        }
        /// <summary>
        /// 加载门的子节点，包括底节点。
        /// </summary>
        private void LoadSubNode()
        {
            foreach (NameCodeType nct in m_nodeInfo.ContainsNodes)
            {
                //SyswareNode sn = mDiagram .Items .Where (w => w.)
                SyswareNode sn = GetNode(nct);
                if (sn == null)
                    continue;
                AddRow(sn);
            }
        }
        private void AddRow(SyswareNode sn)
        {
            NodeInfos ni = sn.Tag as NodeInfos;
            int index = this.dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = ni.Code;
            dataGridView1.Rows[index].Cells[1].Value = ni.Gzl;
            dataGridView1.Rows[index].Cells[2].Value = ni.Fpgl;

        }
        private SyswareNode GetNode(NameCodeType nct)
        {
            SyswareNode ret = null;
            foreach (DiagramItem di in mDiagram.Items)
            {
                if (di.GetType().Name != "SyswareNode")
                    continue;
                SyswareNode sn = (SyswareNode)di;
                NodeInfos ni = sn.Tag as NodeInfos;
                if (ni.Code == nct.Code)
                { ret = sn; break; }
            }
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_nodeInfo.Name = this.textBoxJDMC.Text;
            m_nodeInfo.Type = SetType();
            m_nodeInfo.Fpgl = this.textBoxFPGL.Text;
            m_nodeInfo.Glms = this.richTextBoxGLMS.Text;
            m_nodeInfo.Jdms = this.richTextBoxJDMS.Text;
            m_nodeInfo.isPager = this.checkBoxFY.Checked;
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

        // 分配计算。
        private void buttonCal_Click(object sender, EventArgs e)
        {

        }
    }
}
