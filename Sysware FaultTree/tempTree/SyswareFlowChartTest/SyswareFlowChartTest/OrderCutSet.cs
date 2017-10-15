using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyswareFlowChartTest
{
    class OrderCutSet
    {
        /// <summary>
        /// 阶数
        /// </summary>
        public string orderNum { set; get; }
        /// <summary>
        /// 割集个数
        /// </summary>
        public int cutSetNum { set; get; }
        /// <summary>
        /// 最小割集
        /// </summary>
        public string[] minCutSetName { set; get; }
    }

    class Compare : IEqualityComparer<OrderCutSet>
    {
        public bool Equals(OrderCutSet x, OrderCutSet y)
        {
            return x.orderNum == y.orderNum;//可以自定义去重规则，此处将Id相同的就作为重复记录，不管学生的爱好是什么
        }
        public int GetHashCode(OrderCutSet obj)
        {
            return obj.orderNum.GetHashCode();
        }
    }
}
