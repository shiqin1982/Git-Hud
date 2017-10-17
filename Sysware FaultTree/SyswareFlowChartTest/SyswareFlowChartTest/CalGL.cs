using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SyswareFlowChartTest
{
    public class CalGL
    {
        /// <summary>
        /// 概率分配评分矩阵
        /// </summary>
        private List<List<double>> m_dataMatrix;
        /// <summary>
        /// 当前节点的分配概率
        /// </summary>
        private double m_fpgl;
        /// <summary>
        /// 当前节点的类型
        /// </summary>
        private NodeType m_nodeType;
        /// <summary>
        /// 子节点的权重
        /// </summary>
        public List<double> m_QZ;
        /// <summary>
        /// 子节点的分配概率
        /// </summary>
        public List<double> m_sub_fpgl;
        public CalGL(double _fpgl, List<List<double>> _data, NodeType _nodeType)
        {
            m_fpgl = _fpgl*1e-6;
            m_dataMatrix = _data;
            m_nodeType = _nodeType;
            m_QZ = new List<double>();
            m_sub_fpgl = new List<double>();
            Calculate();
        }
        private void Calculate()
        {
            /// 计算ai
            List<double> ai = new List<double>();
            for (int i = 0; i < m_dataMatrix.Count(); i++)
                ai.Add(m_dataMatrix[i].Sum());
            /// 计算a
            double a = ai.Sum();
            /// 计算权重
            foreach (double tmp in ai)
                m_QZ.Add(tmp / a);

            if (m_nodeType == NodeType.或门)
            {
                foreach (double tmp in m_QZ)
                    m_sub_fpgl.Add(m_fpgl * tmp);
            }
            else if (m_nodeType == NodeType.与门)
            {
                double mult = 1;
                for (int i = 0; i < m_QZ.Count(); i++)
                    mult = mult * m_QZ[i];
                for (int i = 0; i < m_QZ.Count(); i++)
                    m_sub_fpgl.Add(m_fpgl * m_QZ[i] / mult);
            }
            else
            { throw new Exception("不支持的门类型"); }
        }

    }
}

