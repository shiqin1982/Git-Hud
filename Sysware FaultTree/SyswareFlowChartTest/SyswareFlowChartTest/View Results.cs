using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SyswareFlowChartTest.Tools;

namespace SyswareFlowChartTest
{
    public partial class View_Results : Form
    {
        private List<OrderCutSet> _orderCSList = null;
        /// <summary>
        /// 所有最小割集
        /// </summary>
        private string[] _sArray = null;

        List<ProResult> _prList = null;

        private string _resultCutSet = "";
        public View_Results()
        {
            InitializeComponent();
        }

        public void setData(string resultCutSet, double topProbability, string bottomImportance, List<ProResult> prList)
        {
            _resultCutSet = resultCutSet;
            _prList = prList;
            textBox1.Text = "平均每飞行小时失效概率为：\r\n" + topProbability.ToString("#.##E+000");
            DataSet ds1 = Data2Conversion.ConvertXml2DataSet(resultCutSet);
            DataSet ds3 = Data2Conversion.ConvertXml2DataSet(bottomImportance);

            DataTable dt3 = ds3.Tables[0];

            getAllCutSetArray(resultCutSet);
            DataTable dt2 = Data2Conversion.Array2DataTable("cutSet", _sArray);
            getNameTable(dt2);
            dt3 = UpdateDataTable(dt3);
            getOrderNum(resultCutSet);
            getCutSetAndNum();

            dataGridView1.DataSource = _orderCSList;
            dataGridView2.DataSource = dt2;
            dataGridView3.DataSource = dt3;

            dataGridView4.DataSource = prList;
            

        }
        private void View_Results_Load(object sender, EventArgs e)
        {
            dataGridView4.AutoGenerateColumns = false;
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string orderNum = this.dataGridView1.CurrentRow.Cells["Column1"].Value.ToString();
            if ("All".Equals(orderNum))
            {
                if (_sArray == null)
                {
                    getAllCutSetArray(_resultCutSet);
                }
                else
                {

                }
                DataTable dt2 = Data2Conversion.Array2DataTable("cutSet", _sArray);
                getNameTable(dt2);
                dataGridView2.DataSource = dt2;
            }
            else
            {
                List<string> b = new List<string>();
                foreach (string i in _sArray)
                {
                    string [] a = i.Split(',');
                    if ((a.Length).ToString().Equals(orderNum))
                    {
                        b.Add(i);
                    }
                }
                DataTable dt2 = createTable(b);
                getNameTable(dt2);
                dataGridView2.DataSource = dt2;
            }
        }

        private DataTable createTable(List<string> list)
        {
            DataTable dt = new DataTable("cutset");
            DataColumn dc1 = new DataColumn("cutSet");

            dt.Columns.Add(dc1);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["cutSet"] = list[i];
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 转化重要度DataTable  格式
        /// </summary>
        /// <param name="argDataTable"></param>
        /// <returns></returns>
        private DataTable UpdateDataTable(DataTable argDataTable)
        {
            DataTable dtResult = new DataTable();
            //克隆表结构
            dtResult = argDataTable.Clone();
            foreach (DataColumn col in dtResult.Columns)
            {
                if (col.ColumnName == "PrbI")
                {
                    //修改列类型
                    col.DataType = typeof(double);
                }
                if (col.ColumnName == "FVI")
                {
                    //修改列类型
                    col.DataType = typeof(double);
                }
                if (col.ColumnName == "StrtI")
                {
                    //修改列类型
                    col.DataType = typeof(double);
                }
                
            }
            foreach (DataRow row in argDataTable.Rows)
            {
                foreach (ProResult p in _prList)
                {
                    if (row["i"].ToString() == p.Code)
                    {
                        row["i"] = p.Name;
                    }
                }
                DataRow rowNew = dtResult.NewRow();
                rowNew["i"] = row["i"];
                rowNew["PrbI"] = row["PrbI"];
                rowNew["FVI"] = row["FVI"];
                rowNew["StrtI"] = row["StrtI"];
                
                dtResult.Rows.Add(rowNew);
            }
            return dtResult;
        }

        /// <summary>
        /// 获取all阶数,
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private void getOrderNum(string xml)
        {
            _orderCSList = new List<OrderCutSet>();
            OrderCutSet oc = new OrderCutSet();
            oc.orderNum = "All";
            oc.cutSetNum = getAllOrderNum(xml, "<CutSet>");
            oc.minCutSetName = _sArray;
            _orderCSList.Add(oc);
        }
        /// <summary>
        /// 获取all的割集个数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private int getAllOrderNum(string str, string keyWord)
        {
            int count = 0;
            int index = 0;
            while ((index = str.IndexOf(keyWord, index)) != -1)
            {
                count++;
                index = index + keyWord.Length;
            }

            return count;
        }

        /// <summary>
        /// 获取所有最小割集
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private void getAllCutSetArray(string xml)
        {

            xml = xml.Replace("</CutSets>", "");
            xml = xml.Replace("<CutSets>", "");
            xml = xml.Replace("\r\n", "");

            _sArray = Regex.Split(xml, "</CutSet>  <CutSet>", RegexOptions.IgnoreCase);

            for (int i = 0; i < _sArray.Length; i++)
            {
                _sArray[i] = _sArray[i].Replace("\r\n", "");
                _sArray[i] = _sArray[i].Replace("</CutSet>", "");
                _sArray[i] = _sArray[i].Replace("<CutSet>", "");
                _sArray[i] = _sArray[i].Replace("</Evt>    <Evt>", ",");
                _sArray[i] = _sArray[i].Replace("</Evt>", "");
                _sArray[i] = _sArray[i].Replace("<Evt>", "");
                _sArray[i] = _sArray[i].Trim();
            }
        }
        /// <summary>
        /// 获取阶数及割集个数
        /// </summary>
        private void getCutSetAndNum()
        {
            //阶数
            for (int i = 0; i < _sArray.Length; i++)
            {
                string[] newArray = Regex.Split(_sArray[i], ",", RegexOptions.IgnoreCase);
                OrderCutSet ocs = new OrderCutSet();
                ocs.orderNum = newArray.Length.ToString();
                ocs.cutSetNum = 0;
                _orderCSList.Add(ocs);
            }
            //计算割集个数

            for (int k = 0; k < _orderCSList.Count; k++)
            {

                for (int m = 0; m < _orderCSList.Count; m++)
                {
                    if (_orderCSList[m].orderNum == _orderCSList[k].orderNum && !_orderCSList[m].orderNum.Equals("All"))
                    {
                        _orderCSList[m].cutSetNum++;
                    }
                }           
            }

            _orderCSList = _orderCSList.Distinct(new Compare()).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 根据code获取name
        /// </summary>
        /// <returns></returns>
        private void getNameTable(DataTable dt)
        {
            foreach (DataRow r in dt.Rows)
            {
                string[] a = r["cutSet"].ToString().Split(',');

                for (int i = 0; i < a.Length; i++)
                {
                    foreach (ProResult p in _prList)
                    {
                        if (a[i] == p.Code)
                        {
                            a[i] = p.Name;
                        }
                    }
                }
                string str = string.Join(",", a);//数组转成字符串

                r["cutSet"] = str;
            }
        }
    }
}