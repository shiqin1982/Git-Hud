using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyswareFlowChartTest
{
    public class SyswareTreeNode
    {
        public string name { set; get; }
        public string code { set; get; }

        public string pCode { set; get; }

        public string pName { set; get; }
        public string text { set; get; }

        public bool isPager { set; get; }
    }
}
