using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MindFusion.Diagramming;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;

namespace SyswareFlowChartTest
{
    /// <summary>
    /// 顶属性
    /// </summary>
    [Serializable]
    public class NodeInfos
    {
        //private string m_name = null;
        //private string m_code = null;
        //private string m_type = null;
        //private string m_fpgl = null;
        //private string m_glms = null;
        //private string m_fxxs = null;
        //private string m_pjsxgl = null;
        //private string m_jdms = null;
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 节点编码
        /// </summary>
        public string Code
        {
            get;
            set;
        }
        /// <summary>
        /// 门类型
        /// </summary>
        public NodeType Type
        {
            get;
            set;
        }
        /// <summary>
        /// 底事件类型
        /// </summary>
        public AffairType AffaType
        {
            get;
            set;
        }
        /// <summary>
        /// 分配概率
        /// </summary>
        public string Fpgl
        {
            get;
            set;
        }
        /// <summary>
        /// 概率描述
        /// </summary>
        public string Glms
        {
            get;
            set;
        }
        /// <summary>
        /// 飞行小时
        /// </summary>
        public string Fxxs
        {
            get;
            set;
        }
        /// <summary>
        /// 平均失效概率
        /// </summary>
        public string Pjsxgl
        {
            get;
            set;
        }
        /// <summary>
        /// 节点描述
        /// </summary>
        public string Jdms
        {
            get;
            set;
        }
        /// <summary>
        /// 包含的门/底节点code
        /// </summary>
        public NameCodeType.Collection ContainsNodes
        {
            get;

            set;
        }
        /// <summary>
        /// 父节点code
        /// </summary>
        public string ParentCode
        {
            get;
            set;
        }
        /// <summary>
        /// 检视周期
        /// </summary>
        public string Jszq
        {
            get;
            set;
        }
        /// <summary>
        /// 故障率
        /// </summary>
        public string Gzl
        {
            get;
            set;
        }
        /// <summary>
        /// 节点类型
        /// </summary>
        public string ItemType
        {
            get;
            set;
        }
        /// <summary>
        /// 模型逻辑
        /// </summary>
        public string Mxlj
        {
            get;
            set;
        }
        
        public NodeInfos(string code)
        {
            this.Code = code;
        }
    }
    [Serializable]
    public class NameCodeType
    {
        [Serializable]
        public class Collection : Collection<NameCodeType>
        {
            public NameCodeType this[string code]
            {
                get
                {
                    foreach (NameCodeType cnv in this)
                    {
                        if (cnv.Code == code) return cnv;
                    }
                    return null;
                }
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        public NameCodeType(string code,string type)
        {
            this.Code = code;
            this.Type = type;
        }
    }
    /// <summary>
    /// 门属性
    /// </summary>
    [Serializable]
    public class DoorNodeInfos
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 节点编码
        /// </summary>
        public string Code
        {
            get;
            set;
        }
        /// <summary>
        /// 门类型
        /// </summary>
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 分配概率
        /// </summary>
        public string Fpgl
        {
            get;
            set;
        }
        /// <summary>
        /// 概率描述
        /// </summary>
        public string Glms
        {
            get;
            set;
        }
        /// <summary>
        /// 节点描述
        /// </summary>
        public string Jdms
        {
            get;
            set;
        }
        /// <summary>
        /// 父节点code
        /// </summary>
        public string ParentCode
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 底属性
    /// </summary>
    [Serializable]
    public class BotNodeInfos
    {
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 节点编码
        /// </summary>
        public string Code
        {
            get;
            set;
        }
        /// <summary>
        /// 门类型
        /// </summary>
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 分配概率
        /// </summary>
        public string Fpgl
        {
            get;
            set;
        }
        /// <summary>
        /// 概率描述
        /// </summary>
        public string Glms
        {
            get;
            set;
        }
        /// <summary>
        /// 检视周期
        /// </summary>
        public string Jszq
        {
            get;
            set;
        }
        /// <summary>
        /// 故障率
        /// </summary>
        public string Gzl
        {
            get;
            set;
        }
        /// <summary>
        /// 节点描述
        /// </summary>
        public string Jdms
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 门类型
    /// </summary>
    /// 
    [Serializable]
    public enum NodeType
    {
        与门 = 1,//"或门",
        或门 = 2,//"与门",
        优先与门 = 3,//"优先与门",
        禁止门 = 4,//"禁止门",       
    };   
    /// <summary>
    /// 事件类型
    /// </summary>
    /// 
    [Serializable]
    public enum AffairType
    {
        基本事件 = 1,//
        房型事件 = 2,//
        未展开事件 = 3,//
        条件事件 = 4,//
        隐蔽事件 = 5,//
    };
   
    public class BaseCommonFunc
    {
        /// <summary>
        /// 序列化Diagram节点及线信息
        /// </summary>
        /// <param name="dri"></param>
        /// <returns></returns>
        public byte[] SaveFileInfo(DiagramNodeCollection dia)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, dia);
            ms.Close();
            return ms.ToArray();
        }
        /// <summary>
        /// 反序列化Diagram节点及线信息
        /// </summary>
        /// <param name="bts"></param>
        /// <returns></returns>
        public Diagram GetFileInfo(byte[] bts)
        {
            MemoryStream ms = new MemoryStream(bts, 0, bts.Length);
            BinaryFormatter bf = new BinaryFormatter();
            Diagram cd = (Diagram)bf.Deserialize(ms);
            ms.Close();
            return cd;
        }
    }
}
