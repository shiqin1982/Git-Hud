using MindFusion.Diagramming;
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
    public partial class SetTopNode : Form
    {
        public NodeInfos m_nodeInfo = null;
        private Diagram mDiagram;

        public SetTopNode(NodeInfos nodeInfo, Diagram diagram)
        {
            InitializeComponent();
            this.textBoxFPGL.LostFocus += textBox_LostFocus;
            this.textBoxFXXS.LostFocus += textBox_LostFocus;
            m_nodeInfo = nodeInfo;
            mDiagram = diagram;
        }

        void textBox_LostFocus(object sender, EventArgs e)
        {
            VerificationHelper.textBoxVer((TextBox)sender, button1);
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
            LoadSubNode();
        }

        /// <summary>
        /// 加载门的子节点，包括底节点。
        /// </summary>
        private void LoadSubNode()
        {
            foreach (var link in mDiagram.Links.Where(w => w.Origin == mDiagram.ActiveItem))
            {
                SyswareNode next = (SyswareNode)link.Destination;
                AddRow(next);
            }
        }
        private void AddRow(SyswareNode sn)
        {
            NodeInfos ni = sn.Tag as NodeInfos;
            int index = this.dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = ni.Code;
            dataGridView1.Rows[index].Cells[1].Value = ni.Name;
            dataGridView1.Rows[index].Cells[2].Value = ni.glFZD;
            dataGridView1.Rows[index].Cells[3].Value = ni.glBCSD;
            dataGridView1.Rows[index].Cells[4].Value = ni.glZYD;
            dataGridView1.Rows[index].Cells[5].Value = ni.glZHQZ;
            if (ni.Fpgl != null && ni.Fpgl.Trim() != "")
                dataGridView1.Rows[index].Cells[6].Value = double.Parse(ni.Fpgl) / 1e6;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBoxFPGL.BackColor == Macro.verColor || textBoxFXXS.BackColor == Macro.verColor)
            {
                MsgForm mf = new MsgForm("您输入有误，请检查输入内容！");
                mf.ShowDialog();
            }
            else
            {
                m_nodeInfo.Name = this.textBoxJDMC.Text;
                m_nodeInfo.Type = SetType();
                m_nodeInfo.Fxxs = this.textBoxFXXS.Text;
                m_nodeInfo.Fpgl = this.textBoxFPGL.Text;
                m_nodeInfo.Glms = this.richTextBoxGLMS.Text;
                m_nodeInfo.Pjsxgl = this.textBoxPJSXGL.Text;
                m_nodeInfo.Jdms = this.richTextBoxJDMS.Text;
                //this.comboBoxMLX.Text = m_nodeInfo.Type.ToString();
                SaveSubNodeInfo();
            }

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

        /// <summary>
        ///  分配计算。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCal_Click(object sender, EventArgs e)
        {
            this.dataGridView1.EndEdit();
            MsgForm mf;
            if (string.IsNullOrEmpty(this.textBoxFPGL.Text))
            {
                mf = new MsgForm("请填写完常规选项中的分配概率！");
                mf.ShowDialog();
                return;
            }
            DataTable dt = DataGridViewHelper.GetDgvToTable(this.dataGridView1);
            DataView dv = new DataView(dt);

            string notinStr = " not in ('1','2','3','4','5') ";
            string filter = "复杂度" + notinStr + "or 不成熟度" + notinStr + "or 重要度" + notinStr;
            dv.RowFilter = filter;

            if (dv.ToTable().Rows.Count > 0)
            {
                mf = new MsgForm("复杂度或不成熟度或重要度输入格式不正确，请输入1-5的数字！");
                mf.ShowDialog();
                return;
            }


            List<List<double>> lld = GetGridViewData();
            double fpgl = double.Parse(this.textBoxFPGL.Text.Trim());
            try
            {
                SetType();
                CalGL cgl = new CalGL(fpgl, lld, m_nodeInfo.Type);
                ShowData(cgl);
            }
            catch (Exception exp)
            {
                MsgForm emf = new MsgForm(exp.Message, false, "计算出错。");
                emf.ShowDialog();
            }

        }
        private List<List<double>> GetGridViewData()
        {
            List<List<double>> ret = new List<List<double>>();
            int count = dataGridView1.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                List<double> row = new List<double>();
                for (int j = 2; j < 5; j++)
                {
                    row.Add(double.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString()));
                }
                ret.Add(row);
            }
            return ret;
        }

        private void ShowData(CalGL cgl)
        {
            int count = dataGridView1.Rows.Count;
            if (count != cgl.m_sub_fpgl.Count())
                return;
            for (int i = 0; i < count; i++)
            {
                dataGridView1.Rows[i].Cells[5].Value = cgl.m_QZ[i].ToString("f3");
                dataGridView1.Rows[i].Cells[6].Value = cgl.m_sub_fpgl[i].ToString("#.###E+00");
            }
        }
        private void SaveSubNodeInfo()
        {
            dataGridView1.EndEdit();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string code = dataGridView1.Rows[i].Cells[0].Value == null ? "" : dataGridView1.Rows[i].Cells[0].Value.ToString();
                SyswareNode sn = GetNodeByCode(code);
                if (sn == null)
                    continue;
                SaveData(sn, i);
            }
        }
        private void SaveData(SyswareNode sn, int row)
        {
            NodeInfos ni = sn.Tag as NodeInfos;
            ni.glFZD = dataGridView1.Rows[row].Cells[2].Value == null ? "" : dataGridView1.Rows[row].Cells[2].Value.ToString();
            ni.glBCSD = dataGridView1.Rows[row].Cells[3].Value == null ? "" : dataGridView1.Rows[row].Cells[3].Value.ToString();
            ni.glZYD = dataGridView1.Rows[row].Cells[4].Value == null ? "" : dataGridView1.Rows[row].Cells[4].Value.ToString();
            ni.glZHQZ = dataGridView1.Rows[row].Cells[5].Value == null ? "" : dataGridView1.Rows[row].Cells[5].Value.ToString();
            ni.Fpgl = dataGridView1.Rows[row].Cells[6].Value == null ? "" : dataGridView1.Rows[row].Cells[6].Value.ToString();
            if (ni.Fpgl != "")
                ni.Fpgl = (double.Parse(ni.Fpgl) * 1e6).ToString("f3");
        }
        private SyswareNode GetNodeByCode(string code)
        {
            SyswareNode ret = null;
            foreach (DiagramItem di in mDiagram.Items)
            {
                if (di.GetType().Name != "SyswareNode")
                    continue;
                SyswareNode sn = (SyswareNode)di;
                NodeInfos ni = sn.Tag as NodeInfos;
                if (ni.Code == code)
                { ret = sn; break; }
            }
            return ret;
        }

        private void textBoxFPGL_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Back)
            //{
            //    return;
            //}
            //double outDb = 0;
            //if (double.TryParse(textBoxFPGL.Text + e.KeyChar.ToString(), out outDb))
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                return;
            }
            if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                double outDb = 0;
                if (double.TryParse(e.FormattedValue.ToString(), out outDb))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;//数据格式不正确则还原
                    dataGridView1.CancelEdit();
                }
            }
            else
            {
                string[] inPutStr = { "1", "2", "3", "4", "5" };
                if (inPutStr.Contains(e.FormattedValue.ToString()))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;//数据格式不正确则还原
                    dataGridView1.CancelEdit();
                }
            }
        }

    }

}
