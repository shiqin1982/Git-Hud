using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MindFusion.Diagramming;
using System.Drawing.Drawing2D;
using MindFusion.Diagramming.Layout;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using FT2GenieBN;
using FT2Cutset;
using MindFusion.Drawing;
using SyswareFlowChartTest.Tools;

namespace SyswareFlowChartTest
{
    public partial class MainPage : Form
    {
        #region 变量和初始化
        private string m_FilePath = null;
        private RectangleF m_nodeBounds = RectangleF.Empty;
        //private int m_doorSequ = 1;
        //private int m_botSequ = 1;
        private string TopCode = "Top";
        //private int m_pasteDX = 0;
        //private int m_pasteDY =0;
        private string m_testSnode = null;//临时节点编码信息（复杂）
        private XmlDocument m_XmlDoc = null;

        public MainPage()
        {
            InitializeComponent();
            m_nodeBounds = new RectangleF(10, 0, 25, 25);
            diagram1.Tag = new NodeSequence();
            MindFusion.Diagramming.Diagram.RegisterItemClass(typeof(SyswareNode), "10000201", 2);
        }
        private void MainPage_Load(object sender, EventArgs e)
        {
            this.diagramView1.Behavior = Behavior.SelectOnly;
            //this.diagram1.NodeEffects.Add(new GlassEffect());
            //this.diagram1.NodeEffects.Add(new AeroEffect());

            //Rectangle rect = new Rectangle(0, 0, 75, 75);
            initTreeView();
        }
        /// <summary>
        /// 节点排列样式
        /// </summary>
        private void UserLayOut()
        {
            TreeLayout layout = new TreeLayout();
            layout.Type = TreeLayoutType.Centered;
            layout.Direction = TreeLayoutDirections.TopToBottom;
            layout.LinkStyle = TreeLayoutLinkType.Cascading3;
            layout.NodeDistance = 8;
            layout.LevelDistance = 12; // let horizontal positions overlap

            layout.Arrange(this.diagram1);
        }
        #endregion

        #region 节点和样式
        /// <summary>
        /// 创建顶节点
        /// </summary>
        private void CreateBaseNode()
        {
            this.diagram1.ClearAll();
            //RectangleF nodeBounds = new RectangleF(50, 20, 25, 25);
            //SyswareNode root = this.diagram1.Factory.CreateShapeNode(nodeBounds);

            SyswareNode root = new SyswareNode();
            root.SetBounds(m_nodeBounds, false, false);
            //CreateDoor(NodeType.与门, root);
            CreateDoor(NodeType.或门, root);

            root.Font = new Font("宋体", 9);
            root.Text = "顶事件";

            NodeInfos nodeInfo = new NodeInfos(TopCode);
            nodeInfo.Name = "顶事件";
            nodeInfo.ItemType = "顶";
            nodeInfo.Type = NodeType.或门;
            nodeInfo.isPager = true;
            root.Tag = nodeInfo;

            diagram1.Nodes.Add(root);
            UserLayOut();
            createTopNode();
            //this.diagramView1
        }
        //添加、新建门节点
        private void AddDoorNode(NodeInfos selectRootNodeTag, SyswareNode selectRootNode)
        {
            if (selectRootNodeTag.ItemType == "底")
                return;
            //SyswareNode node =(SyswareNode) this.diagram1.Factory.CreateShapeNode(m_nodeBounds);
            SyswareNode node = new SyswareNode();
            node.SetBounds(m_nodeBounds, false, false);
            CreateDoor(NodeType.与门, node);
            NodeSequence ns = diagram1.Tag as NodeSequence;
            //NodeInfos nodeInfo = new NodeInfos("G" + m_doorSequ.ToString());
            NodeInfos nodeInfo = new NodeInfos("G" + ns.DoorSeq.ToString());
            nodeInfo.Name = "逻辑门事件" + ns.DoorSeq.ToString();
            nodeInfo.ItemType = "门";
            nodeInfo.Type = NodeType.与门;
            nodeInfo.ParentCode = selectRootNodeTag.Code;
            nodeInfo.ContainsNodes = new NameCodeType.Collection();
            node.Tag = nodeInfo;
            node.Font = new Font("宋体", 9);
            //node.Text = "GATE" + m_doorSequ.ToString();
            node.Text = "逻辑门事件" + ns.DoorSeq.ToString();

            selectRootNodeTag.ContainsNodes.Add(new NameCodeType(nodeInfo.Code, nodeInfo.ItemType));
            diagram1.Nodes.Add(node);

            this.diagram1.Factory.CreateDiagramLink(selectRootNode, node);
            UserLayOut();
            //m_doorSequ++;
            ns.DoorSeq++;

        }
        /// <summary>
        /// 创建门节点样式
        /// </summary>
        /// <param name="current"></param>
        private void CreateDoor(NodeType type, SyswareNode node)
        {
            switch (type)
            {
                case NodeType.与门:
                    CreateAndDoorStyle(node);
                    break;
                case NodeType.或门:
                    CreateOrDoorStyle(node);
                    break;
                case NodeType.禁止门:
                    CreateBanDoorStyle(node);
                    break;
                case NodeType.优先与门:
                    CreatePrioAndDoorStyle(node);
                    break;
                default:
                    CreateAndDoorStyle(node);
                    break;
            }
        }

        /// <summary>
        /// 与门样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateAndDoorStyle(SyswareNode node)
        {
            node.Shape = new Shape(
    new ElementTemplate[]
	{        
	},
    new ElementTemplate[] 
	{
		new LineTemplate(50, 45, 50, 40, Color.FromName("Black"), DashStyle.Custom, -1),
		new LineTemplate(40,100,40,105),
		new LineTemplate (60,100,60,105)
	},
    null,
    FillMode.Winding, "test",
    new ShapeDecoration[]
	{
		new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 0, 100, 0),
				new LineTemplate(100, 20, 100, 40),
				new LineTemplate(100, 40, 0, 40),
				new LineTemplate(0, 40, 0, 20)                
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(25, 75, 25, 60),
				new BezierTemplate(25, 60, 25, 40, 75f, 40, 75, 60),
				new LineTemplate(75, 60, 75, 100),
				new LineTemplate(75, 100, 25, 100)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 60, 100, 60),
				new LineTemplate(100, 60, 100, 80),
				new LineTemplate(100, 80, 0, 80),
				new LineTemplate(0, 80, 0, 80)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		)
	}
);

        }
        /// <summary>
        /// 或门样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateOrDoorStyle(SyswareNode node)
        {
            node.Shape = new Shape(
    new ElementTemplate[]
	{
		
	},
    new ElementTemplate[] {
		new LineTemplate(50, 45, 50, 40),
		new LineTemplate(40, 90, 40, 100),
		new LineTemplate(60, 90, 60, 100)
	},
    null,
    FillMode.Winding, "test",
    new ShapeDecoration[]
	{
		new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 0, 100, 0),
				new LineTemplate(100, 20, 100, 40),
				new LineTemplate(100, 40, 0, 40),
				new LineTemplate(0, 40, 0, 20)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255,128,128,255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
		new LineTemplate(25,75,25,60),                     
				new BezierTemplate(25, 60, 25, 40, 75f, 40, 75, 60),
				new LineTemplate(75, 60, 75, 100),
				new LineTemplate(75, 100, 75, 100),
				new BezierTemplate(75, 100, 60, 85, 45f, 85, 25, 100)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255,128,128,255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 60, 100, 60),
				new LineTemplate(100, 60, 100, 80),
				new LineTemplate(100, 80, 0, 80),
				new LineTemplate(0, 80, 0, 80)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255,128,128,255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		)
	}
);

        }
        /// <summary>
        /// 禁止门样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateBanDoorStyle(SyswareNode node)
        {
            node.Shape = new Shape(
    new ElementTemplate[]
	{
		
	},
    new ElementTemplate[] {
		new LineTemplate(50, 45, 50, 40),
		new LineTemplate(25,95,75,95),
		new LineTemplate(40, 100, 40, 105),
		new LineTemplate(60, 100, 60, 105)
	},
    null,
    FillMode.Winding, "test",
    new ShapeDecoration[]
	{
		new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 0, 100, 0),
				new LineTemplate(100, 20, 100, 40),
				new LineTemplate(100, 40, 0, 40),
				new LineTemplate(0, 40, 0, 20)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(25, 75, 25, 60),
				new BezierTemplate(25, 60, 25, 40, 75f, 40, 75, 60),
				new LineTemplate(75, 60, 75, 100),
				new LineTemplate(75, 100, 25, 100)
			   
				//new BezierTemplate(75, 100, 60, 85, 45f, 85, 25, 100)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 60, 100, 60),
				new LineTemplate(100, 60, 100, 80),
				new LineTemplate(100, 80, 0, 80),
				new LineTemplate(0, 80, 0, 80)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		)
	}
);

        }
        /// <summary>
        /// 优先与门样式
        /// </summary>
        /// <param name="node"></param>
        private void CreatePrioAndDoorStyle(SyswareNode node)
        {
            node.Shape = new Shape(
    new ElementTemplate[]
	{
		
	},
    new ElementTemplate[] {
		new LineTemplate(50, 45, 50, 40),
		new LineTemplate(40, 95, 40, 100),
		new LineTemplate(60, 95, 60, 100),
		
	},
    null,
    FillMode.Winding, "test",
    new ShapeDecoration[]
	{
		new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 0, 100, 0),
				new LineTemplate(100, 0, 100, 40),
				new LineTemplate(100, 40, 0, 40),
				new LineTemplate(0, 40, 0, 20)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(20, 55, 50, 45),
				new LineTemplate(50, 45, 80, 55),
				new LineTemplate(80, 55, 80, 90),
				new LineTemplate(80, 90, 50, 100),
				new LineTemplate(50, 100, 20, 90),
				new LineTemplate(20, 90, 20, 60)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 62, 100, 62),
				new LineTemplate(100, 62, 100, 82),
				new LineTemplate(100, 82, 0, 82),
				new LineTemplate(0, 82, 0, 82)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		)
	}
);


        }

        ///<summary>
        ///分页样式
        ///</summary>
        ////// <param name="node"></param>
        private void CreatePagingStyle(SyswareNode node)
        {
            node.Shape = new Shape(
    new ElementTemplate[]
	{
		
	},
    new ElementTemplate[] {
		new LineTemplate(50, 40, 50, 50, Color.FromName("Black"), DashStyle.Custom, -1),
		
	},
    null,
    FillMode.Winding, "test",
    new ShapeDecoration[]
	{
		new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 0, 100, 0),
				new LineTemplate(100, 20, 100, 40),
				new LineTemplate(100, 40, 0, 40),
				new LineTemplate(0, 40, 0, 20)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(75, 90, 25, 90),
				new LineTemplate(25, 90, 50, 50),
				new LineTemplate(50, 50, 75, 90),
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 60, 100, 60),
				new LineTemplate(100, 60, 100, 80),
				new LineTemplate(100, 80, 0, 80),
				new LineTemplate(0, 80, 0, 80)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		)
	}
);
        }
        //添加、新建底节点
        private void AddBotNode(NodeInfos selectRootNodeTag, SyswareNode selectRootNode)
        {
            if (selectRootNodeTag.ItemType == "底")
                return;
            //SyswareNode node = this.diagram1.Factory.CreateShapeNode(m_nodeBounds) as SyswareNode;

            SyswareNode node = new SyswareNode();
            node.SetBounds(m_nodeBounds, false, false);

            CreateBotNode(AffairType.基本事件, node);

            NodeSequence ns = diagram1.Tag as NodeSequence;

            //NodeInfos nodeInfo = new NodeInfos("E" + m_botSequ.ToString());
            NodeInfos nodeInfo = new NodeInfos("E" + ns.EventSeq.ToString());
            nodeInfo.Name = "事件" + ns.EventSeq.ToString();
            nodeInfo.ItemType = "底";
            nodeInfo.AffaType = AffairType.基本事件;
            nodeInfo.ContainsNodes = new NameCodeType.Collection();

            node.Tag = nodeInfo;
            node.Font = new Font("宋体", 9);
            //node.Text = "EVENT" + m_botSequ.ToString();
            node.Text = "事件" + ns.EventSeq.ToString();
            //Thickness tk = new Thickness(0, 11, 0, 0);
            //node.TextPadding = tk;
            //if (!Snode.ContainsNodes.Contains(nodeInfo.Code))
            selectRootNodeTag.ContainsNodes.Add(new NameCodeType(nodeInfo.Code, nodeInfo.ItemType));

            diagram1.Nodes.Add(node);

            this.diagram1.Factory.CreateDiagramLink(selectRootNode, node);
            UserLayOut();
            //m_botSequ++;
            ns.EventSeq++;
        }
        /// <summary>
        /// 创建底节点
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private SyswareNode CreateBotNode(AffairType type, SyswareNode node)
        {
            switch (type)
            {
                case AffairType.基本事件:
                    CreateBaseAffairStyle(node);
                    break;
                case AffairType.条件事件:
                    CreateFiletAffairStyle(node);
                    break;
                case AffairType.未展开事件:
                    CreateUnfoldAffairStyle(node);
                    break;
                case AffairType.房型事件:
                    CreateRoomAffairStyle(node);
                    break;
                case AffairType.隐蔽事件:
                    CreateHideAffairStyle(node);
                    break;
                default:
                    CreateBaseAffairStyle(node);
                    break;
            }

            return node;
        }
        /// <summary>
        /// 基本事件样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateBaseAffairStyle(SyswareNode node)
        {
            node.Shape = new Shape(
  new ElementTemplate[]
	{
		/*new LineTemplate(0, 0, 100, 0),
		new LineTemplate(100, 0, 100, 100),
		new LineTemplate(100, 100, 0, 100),
		new LineTemplate(0, 100, 0, 0)*/
	},
    new ElementTemplate[] {
		new LineTemplate(50, 45, 50, 40),
		//new LineTemplate(40, 90, 40, 100),
		//new LineTemplate(60, 90, 60, 100),
		
	},
    null,
    FillMode.Winding, "test",
    new ShapeDecoration[]
	{
		new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 0, 100, 0),
				new LineTemplate(100, 20, 100, 40),
				new LineTemplate(100, 40, 0, 40),
				new LineTemplate(0, 40, 0, 20)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(20, 75, 20, 60),
				new BezierTemplate(20, 60, 25, 45, 65f, 35, 80, 65),
				//new LineTemplate(80, 60, 80, 80),
				//new LineTemplate(80, 80, 80, 90),
				new BezierTemplate(80, 60, 80, 105, 30f, 105, 20, 80),
				//new LineTemplate(20, 90, 20, 80)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 60, 100, 60),
				new LineTemplate(100, 60, 100, 80),
				new LineTemplate(100, 80, 0, 80),
				new LineTemplate(0, 80, 0, 80)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		 ),new ShapeDecoration(
			 new ElementTemplate[]
			 {
				new LineTemplate(0,45,15,45),
				new LineTemplate (15,45,15,55),
				new LineTemplate(15,55,0,55),
				new LineTemplate(0,55,0,55),
			   
			 },
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		)
	}
);
        }
        /// <summary>
        /// 条件事件样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateFiletAffairStyle(SyswareNode node)
        {
            node.Shape = new Shape(
    new ElementTemplate[]
	{
		/*new LineTemplate(0, 0, 100, 0),
		new LineTemplate(100, 0, 100, 100),
		new LineTemplate(100, 100, 0, 100),
		new LineTemplate(0, 100, 0, 0)*/
	},
    new ElementTemplate[] {
		new LineTemplate(50, 45, 50, 40)
	},
    null,
    FillMode.Winding, "test",
    new ShapeDecoration[]
	{
		new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 0, 100, 0),
				new LineTemplate(100, 20, 100, 40),
				new LineTemplate(100, 40, 0, 40),
				new LineTemplate(0, 40, 0, 20)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(30, 50, 70, 50),
				new LineTemplate(70, 50, 70, 50),
				new BezierTemplate(70, 50, 100, 50, 100f, 90, 70, 90),
				//new LineTemplate(70, 90, 30, 90),
				//new LineTemplate(30, 90, 30, 90),
				new BezierTemplate(30, 90, 0, 80, 0f, 60, 30, 50)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 60, 100, 60),
				new LineTemplate(100, 60, 100, 80),
				new LineTemplate(100, 80, 0, 80),
				new LineTemplate(0, 80, 0, 80)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(
			 new ElementTemplate[]
			 {
				new LineTemplate(0,45,15,45),
				new LineTemplate (15,45,15,55),
				new LineTemplate(15,55,0,55),
				new LineTemplate(0,55,0,55),
			   
			 },
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(
			 new ElementTemplate[]
			 {
				
				//new LineTemplate(0,45,15,45),
				//new LineTemplate (15,45,15,55),
				//new LineTemplate(15,55,0,55),
				//new LineTemplate(0,55,0,55),
			   
			 },
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		)
	}
);
        }
        /// <summary>
        /// 未展开事件样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateUnfoldAffairStyle(SyswareNode node)
        {
            node.Shape = new Shape(
    new ElementTemplate[]
	{
		/*new LineTemplate(0, 0, 100, 0),
		new LineTemplate(100, 0, 100, 100),
		new LineTemplate(100, 100, 0, 100),
		new LineTemplate(0, 100, 0, 0)*/
	},
    new ElementTemplate[] {
		new LineTemplate(50, 45, 50, 40, Color.FromName("Black"), DashStyle.Custom, -1),
	},
    null,
    FillMode.Winding, "test",
    new ShapeDecoration[]
	{
		new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 0, 100, 0),
				new LineTemplate(100, 20, 100, 40),
				new LineTemplate(100, 40, 0, 40),
				new LineTemplate(0, 40, 0, 20)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(20, 60, 50, 45),
				new LineTemplate(50, 45, 80, 60),
				new LineTemplate(80, 70, 80, 80),
				new LineTemplate(80, 80, 50, 100),
				new LineTemplate(50, 100, 20, 80),
				new LineTemplate(20, 80, 20, 70)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 60, 100, 60),
				new LineTemplate(100, 60, 100, 80),
				new LineTemplate(100, 80, 0, 80),
				new LineTemplate(0, 80, 0, 80)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(
			 new ElementTemplate[]
			 {
				new LineTemplate(0,45,15,45),
				new LineTemplate (15,45,15,55),
				new LineTemplate(15,55,0,55),
				new LineTemplate(0,55,0,55),
			 },
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		)
	}
);

        }
        /// <summary>
        /// 房型事件样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateRoomAffairStyle(SyswareNode node)
        {
            node.Shape = new Shape(
  new ElementTemplate[]
	{
		
		/*new LineTemplate(0, 0, 100, 0),
		new LineTemplate(100, 0, 100, 100),
		new LineTemplate(100, 100, 0, 100),
		new LineTemplate(0, 100, 0, 0)*/
	},
    new ElementTemplate[] {
		new LineTemplate(50, 45, 50, 40, Color.FromName("Black"), DashStyle.Custom, -1),
		//new LineTemplate(40, 90, 40, 100, Color.FromName("Black"), DashStyle.Custom, -1),
		//new LineTemplate(60, 90, 60, 100),
		
		
	},

  null,
  FillMode.Winding, "test",
  new ShapeDecoration[]
	{
		new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 0, 100, 0),
				new LineTemplate(100, 20, 100, 40),
				new LineTemplate(100, 40, 0, 40),
				new LineTemplate(0, 40, 0, 20)
			   
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(20, 60, 50, 45),
				new LineTemplate(50, 45, 80, 60),
				new LineTemplate(80, 60, 80, 100),
				new LineTemplate(80, 100, 20, 100),
				//new LineTemplate(20, 90, 20, 70)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 60, 100, 60),
				new LineTemplate(100, 60, 100, 80),
				new LineTemplate(100, 80, 0, 80),
				new LineTemplate(0, 80, 0, 80)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(
			new ElementTemplate[]
			{
				new LineTemplate(0,45,15,45),
				new LineTemplate (15,45,15,55),
				new LineTemplate(15,55,0,55),
				new LineTemplate(0,55,0,55),
			   
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),
	   
	}
);

        }
        /// <summary>
        /// 隐蔽事件样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateHideAffairStyle(SyswareNode node)
        {
            node.Shape = new Shape(
  new ElementTemplate[]
	{
		/*new LineTemplate(0, 0, 100, 0),
		new LineTemplate(100, 0, 100, 100),
		new LineTemplate(100, 100, 0, 100),
		new LineTemplate(0, 100, 0, 0)*/
	},
     new ElementTemplate[] 
	 {
		new LineTemplate(50, 45, 50, 40, Color.FromName("Black"), DashStyle.Custom, -1),
		
	 },
    null,
    FillMode.Winding, "test",
    new ShapeDecoration[]
	{
		new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 0, 100, 0),
				new LineTemplate(100, 20, 100, 40),
				new LineTemplate(100, 40, 0, 40),
				new LineTemplate(0, 40, 0, 20)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{

				new LineTemplate(20, 60, 50, 45),
				new LineTemplate(50, 45, 80, 60),
				new LineTemplate(80, 70, 80, 80),
				new LineTemplate(80, 80, 50, 100),
				new LineTemplate(50, 100, 20, 80),
				new LineTemplate(20, 80, 20, 70)
				//new LineTemplate(20, 60, 50, 45),
				//new LineTemplate(50, 45, 80, 60),
				//new LineTemplate(80, 60, 50, 90),
				//new LineTemplate(50, 90, 20, 60)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(50, 50, 90, 70),
				new LineTemplate(90, 70, 50, 95),
				new LineTemplate(50, 95, 20, 75),
				new LineTemplate(20, 75, 0, 75)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(0, 60, 100, 60),
				new LineTemplate(100, 60, 100, 80),
				new LineTemplate(100, 80, 0, 80),
				new LineTemplate(0, 80, 0, 80)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(
			 new ElementTemplate[]
			 {
				new LineTemplate(0,45,15,45),
				new LineTemplate (15,45,15,55),
				new LineTemplate(15,55,0,55),
				new LineTemplate(0,55,0,55),
			   
			 },
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		)
	}
);

        }
        #endregion

        #region  复制 粘贴
        /// <summary>
        /// 去掉字符串中的数字
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string RemoveNumber(string str)
        {
            return Regex.Replace(str, @"\d", "");
        }
        /// <summary>
        /// 重新设置节点id
        /// </summary>
        /// <param name="nodei"></param>
        /// <returns></returns>
        private string SetCopyNodeCode(NodeInfos nodei)
        {
            string code = RemoveNumber(nodei.Code);
            if (nodei.ItemType == "门")
            {
                NodeSequence ns = diagram1.Tag as NodeSequence;
                //code += m_doorSequ.ToString();
                //m_doorSequ++;
                code += ns.DoorSeq.ToString();
                ns.DoorSeq++;
            }
            else
            {
                NodeSequence ns = diagram1.Tag as NodeSequence;
                code += ns.EventSeq.ToString();
                ns.EventSeq++;
            }
            return code;
        }
        private NodeInfos UpdataNodeInfo(SyswareNode nnode)
        {
            NodeInfos oldNode = nnode.Tag as NodeInfos;
            NodeInfos newNode = oldNode.Clone();
            //NodeInfos newNode = new NodeInfos(oldNode.Code);
            //newNode.AffaType = oldNode.AffaType;
            //newNode.ContainsNodes = oldNode.ContainsNodes;
            //newNode.Fpgl = oldNode.Fpgl;
            //newNode.Fxxs = oldNode.Fxxs;
            //newNode.Glms = oldNode.Glms;
            //newNode.Gzl = oldNode.Gzl;
            //newNode.ItemType = oldNode.ItemType;
            //newNode.Jdms = oldNode.Jdms;
            //newNode.Jszq = oldNode.Jszq;
            //newNode.Name = oldNode.Name;
            //newNode.ParentCode = oldNode.ParentCode;
            //newNode.Pjsxgl = oldNode.Pjsxgl;
            //newNode.Type = oldNode.Type;

            return newNode;
        }
        private void CopyNodes()
        {
            if (this.diagram1.ActiveItem == null)
                return;
            m_stacks = new Dictionary<string, SyswareNode>();
            SyswareNode seleNode = (SyswareNode)this.diagram1.ActiveItem;
            NodeInfos nodei = seleNode.Tag as NodeInfos;
            m_testSnode = nodei.Code.Clone().ToString();

            //SyswareNode nnode = seleNode.Clone(true);
            SyswareNode nnode = seleNode.Copy();
            nnode.Tag = nodei.Clone();
            if (nodei.ItemType == "底")
                CreateBotNode(nodei.AffaType, nnode);
            else
                CreateDoor(nodei.Type, nnode);
            //nnode.Bounds = new RectangleF(seleNode.Bounds.X + 10, seleNode.Bounds.Y + 10, seleNode.Bounds.Width, seleNode.Bounds.Height);

            m_stacks.Add(nodei.Code, nnode);

            GetSubItems((SyswareNode)this.diagram1.ActiveItem, true);
            /*m_pasteDX = 10;
            m_pasteDY = 10;

            if (this.diagram1.ActiveItem == null) return;
            if (this.diagram1.ActiveItem is ShapeNode)
            {
                SyswareNode current = (ShapeNode)this.diagram1.ActiveItem;
                m_testSnode = current.Tag as NodeInfos;
                if (m_testSnode == null) return;
            }
            else if (this.diagram1.ActiveItem is DiagramLink)
            {
                DiagramLink dia = this.diagram1.ActiveItem as DiagramLink;
                //dia.
            }

            this.diagramView1.CopyToClipboard(true);*/
        }
        /// <summary>
        /// 添加复制的线
        /// </summary>
        private void SetNodeLine()
        {
            if (m_stacks == null)
                return;
            foreach (var stack in m_stacks)
            {
                NodeInfos node = stack.Value.Tag as NodeInfos;
                if (node.ContainsNodes.Count >= 0)
                {
                    foreach (NameCodeType nct in node.ContainsNodes)
                    {
                        this.diagram1.Links.Add(new DiagramLink(this.diagram1, stack.Value, m_stacks[nct.Code]));
                    }

                }
                //SetNodeLine((ShapeNode)stack);
            }
        }
        /// <summary>
        /// 粘帖
        /// </summary>
        private void PasteNodes()
        {
            if (this.diagram1.ActiveItem == null || m_stacks == null)
            {
                MsgForm mf = new MsgForm(" 请先复制！");
                mf.ShowDialog();
                return;
            }

            SyswareNode currentSele = (SyswareNode)this.diagram1.ActiveItem;

            foreach (var stack in m_stacks)
            {
                this.diagram1.Nodes.Add(stack.Value);
            }

            SyswareNode oneCurrentSele = m_stacks[m_testSnode];
            this.diagram1.Links.Add(new DiagramLink(this.diagram1, currentSele, oneCurrentSele));

            SetNodeLine();
            UserLayOut();
            m_stacks = null;
            /*SyswareNode current1 = (ShapeNode)this.diagram1.ActiveItem;

            this.diagramView1.PasteFromClipboard(m_pasteDX, m_pasteDY, true);



            if (this.diagram1.ActiveItem == null) 
             * return;
			
            if (this.diagram1.ActiveItem is ShapeNode)
            {
                SyswareNode current = (ShapeNode)this.diagram1.ActiveItem;
                current.Tag = m_testSnode;
            }
            else if (this.diagram1.ActiveItem is DiagramLink)
            {
                DiagramLink dia = this.diagram1.ActiveItem as DiagramLink;
                dia.
            }
            m_pasteDX += 10;
            m_pasteDY += 10;

            SyswareNode current2 = (ShapeNode)this.diagram1.ActiveItem;

            this.diagram1.Links.Add(new DiagramLink(this.diagram1, current1, current2));*/
        }
        /// <summary>
        ///特殊粘帖 
        /// </summary>
        private void SpecialPasteNodes()
        {
            if (this.diagram1.ActiveItem == null || m_stacks == null)
            {
                MsgForm mf = new MsgForm(" 请先复制！");
                mf.ShowDialog();
                return;
            }

            SyswareNode currentSele = (SyswareNode)this.diagram1.ActiveItem;

            NodeInfos test = null;
            foreach (var stack in m_stacks)
            {
                test = stack.Value.Tag as NodeInfos;
                test.Code = SetCopyNodeCode(test);
                this.diagram1.Nodes.Add(stack.Value);
            }

            SyswareNode oneCurrentSele = m_stacks[m_testSnode];
            if (oneCurrentSele == null)
                return;
            this.diagram1.Links.Add(new DiagramLink(this.diagram1, currentSele, oneCurrentSele));
            SetNodeLine();
            UserLayOut();
            m_stacks = null;
        }
        /// <summary>
        /// 剪切
        /// </summary>
        private void CutNodes()
        {
            if (this.diagram1.ActiveItem == null)
                return;
            m_stacks = new Dictionary<string, SyswareNode>();
            SyswareNode seleNode = (SyswareNode)this.diagram1.ActiveItem;
            NodeInfos nodei = seleNode.Tag as NodeInfos;
            m_testSnode = nodei.Code.Clone().ToString();

            //SyswareNode nnode = (ShapeNode)seleNode.Clone(true);
            //nnode.Bounds = new RectangleF(seleNode.Bounds.X + 10, seleNode.Bounds.Y + 10, seleNode.Bounds.Width, seleNode.Bounds.Height);

            m_stacks.Add(nodei.Code, seleNode);

            GetSubItems((SyswareNode)this.diagram1.ActiveItem, false);

            foreach (var item in m_stacks)
            {
                this.diagram1.Items.Remove((DiagramItem)(item.Value));
            }
        }

        private void GetSubItems(DiagramItem current, Stack<DiagramItem> stacks)
        {
            foreach (var link in this.diagram1.Links.Where(w => w.Origin == current))
            {
                DiagramItem next = (DiagramItem)link.Destination;
                stacks.Push(next);
                GetSubItems(next, stacks);
            }
        }
        private void GetSubItems(SyswareNode current, bool isCopy)
        {
            foreach (var link in this.diagram1.Links.Where(w => w.Origin == current))
            {
                SyswareNode next = (SyswareNode)link.Destination;
                NodeInfos nodei = next.Tag as NodeInfos;
                if (isCopy)
                {
                    SyswareNode nshape = next.Copy();
                    nshape.Bounds = new RectangleF(next.Bounds.X + 10, next.Bounds.Y + 10, next.Bounds.Width, next.Bounds.Height);
                    //nshape.Tag = UpdataNodeInfo(nshape);

                    nshape.Tag = nodei.Clone();
                    if (nodei.ItemType == "底")
                        CreateBotNode(nodei.AffaType, nshape);
                    else
                        CreateDoor(nodei.Type, nshape);

                    m_stacks.Add(nodei.Code, nshape);//. Push(next);
                }
                else
                    m_stacks.Add(nodei.Code, next);//. Push(next);
                GetSubItems(next, isCopy);
            }
        }
        /// <summary>
        /// 递归刷新节点
        /// </summary>
        private void OpenFileRefresh(SyswareNode topNode)
        {
            foreach (var link in this.diagram1.Links.Where(w => w.Origin == topNode))
            {
                SyswareNode next = (SyswareNode)link.Destination;
                NodeInfos nodeInfo = next.Tag as NodeInfos;
                if (nodeInfo.ItemType == "底")
                    CreateBotNode(nodeInfo.AffaType, next);
                else
                    CreateDoor(nodeInfo.Type, next);

                OpenFileRefresh(next);
            }
        }
        #endregion

        #region  鼠标/工具栏 及右键响应
        //新建
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NodeSequence ns = diagram1.Tag as NodeSequence;
            if (this.diagram1.Nodes.Count() == 0)
            {
                //this.diagramView1.Behavior = Behavior.DrawLinks;
                m_FilePath = null;
                this.diagram1.NodeEffects.Add(new GlassEffect());
                this.diagram1.NodeEffects.Add(new AeroEffect());
                
                ns.DoorSeq = 1;
                ns.EventSeq = 1;
                CreateBaseNode();
            }
            else
            {
                MsgForm mf = new MsgForm("新建后当前文件将被覆盖！", true);
                DialogResult dr = mf.ShowDialog();
                {
                    if (dr == DialogResult.OK)
                    {
                        m_FilePath = null;
                        //this.diagramView1.Behavior = Behavior.DrawLinks;
                        this.diagram1.NodeEffects.Add(new GlassEffect());
                        this.diagram1.NodeEffects.Add(new AeroEffect());

                        ns.DoorSeq = 1;
                        ns.EventSeq = 1;
                        CreateBaseNode();
                    }
                    else if (dr == DialogResult.Cancel)
                    {
                        //用户选择取消的操作
                    }
                }
            }
        }
        //打开
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "xml文件 (*.xml)|*.xml|所有文件 (*.*)|*.*";
                //dlg.Filter = "psa文件 (*.psa)|*.psa";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.diagram1.ClearAll();
                    string ext = System.IO.Path.GetExtension(dlg.FileName).ToLower();
                    if (ext == ".psa")
                    {
                        m_FilePath = dlg.FileName;
                        Stream stre = new FileStream(dlg.FileName, FileMode.Open);

                        this.diagramView1.LoadFromStream(stre);// LoadFromFile(dlg.FileName);//打开文件"E:\\杨稳\\11.psa"
                        stre.Close();
                        stre.Dispose();
                        // this.diagram1.Nodes[0].

                        //SyswareNode top = (SyswareNode)this.diagram1.Nodes[0];
                        SyswareNode top = SyswareNode.Initial((ShapeNode)this.diagram1.Nodes[0]);
                        NodeInfos nodeinfo = top.Tag as NodeInfos;
                        if (nodeinfo != null)
                        {
                            CreateDoor(nodeinfo.Type, top);
                            OpenFileRefresh(top);
                        }
                    }
                    else if (ext == ".xml")
                    {
                        m_FilePath = dlg.FileName;
                        this.diagramView1.LoadFromXml(dlg.FileName);
                        SyswareNode top = (SyswareNode)this.diagram1.Nodes[0];
                        // SyswareNode top = SyswareNode.Initial((ShapeNode)this.diagram1.Nodes[0]);
                        NodeInfos nodeinfo = top.Tag as NodeInfos;
                        if (nodeinfo != null)
                        {
                            CreateDoor(nodeinfo.Type, top);
                            OpenFileRefresh(top);
                        }
                    }
                    rePaintTreeNode();
                }
            }
        }
        //保存
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (m_FilePath == null)
            {
                ToolStripMenuItemSaveTo_Click(sender, e);
            }
            else
            {
                string name = m_FilePath;
                if (File.Exists(name))
                    File.Delete(name);
                // this.diagramView1.SaveToFile(name + ".txt", true);
                this.diagramView1.SaveToXml(name);
            }
        }
        //另存为
        private void ToolStripMenuItemSaveTo_Click(object sender, EventArgs e)
        {
            //this.diagramView.SaveToFile("E:\\杨稳\\11.psa", true);//另存为文件
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "xml文件 (*.xml)|*.xml";
                dlg.OverwritePrompt = false;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_FilePath = dlg.FileName;
                    if (System.IO.File.Exists(dlg.FileName))
                        System.IO.File.Delete(dlg.FileName);
                    this.diagramView1.SaveToXml(dlg.FileName);
                }
            }
        }
        //门节点
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.UpArrow;
            this.toolStripButton4.Checked = true;
            this.toolStripButton5.Checked = false;
            this.toolStripButton4.ForeColor = Color.Red;
            this.toolStripButton5.ForeColor = Color.Black;

            //this.toolStripButton5.CheckOnClick = false;
            //this.toolStripButton4.
            //CreateBaseNode();
        }
        //底节点
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.NoMove2D;
            this.toolStripButton4.Checked = false;
            this.toolStripButton5.Checked = true;
            this.toolStripButton5.ForeColor = Color.Red;
            this.toolStripButton4.ForeColor = Color.Black;
            //this.toolStripButton4.CheckOnClick = false;
        }
        //恢复
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.Default;
            this.toolStripButton4.Checked = false;
            this.toolStripButton5.Checked = false;
            this.toolStripButton4.ForeColor = Color.Black;
            this.toolStripButton5.ForeColor = Color.Black;
        }
        //打印(生成xml文档)
        private void toolStripButton13_Click_1(object sender, EventArgs e)
        {

        }

        //放大
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.diagramView1.ZoomIn();
        }
        //缩小
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            this.diagramView1.ZoomOut();
        }
        //鼠标操作放大/缩小
        private void diagramView1_MouseWheel(object sender, MouseEventArgs e)
        {
            diagramView1.ZoomFactor += e.Delta / 40;
        }
        //还原
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            this.diagramView1.ZoomToFit();
        }

        //双击画板
        private void diagramView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.diagram1.ActiveItem == null) return;
            if (this.diagram1.ActiveItem.GetType().Name != "SyswareNode") return;
            SyswareNode current = (SyswareNode)this.diagram1.ActiveItem;
            NodeInfos Snode = current.Tag as NodeInfos;
            if (Snode == null) return;
            if (Snode.ContainsNodes == null)
                Snode.ContainsNodes = new NameCodeType.Collection();
            if (this.toolStripButton4.Checked)//添加门节点
            {
                AddDoorNode(Snode, current);
            }
            else if (this.toolStripButton5.Checked)//添加底节点
            {
                AddBotNode(Snode, current);
            }
            /*//创建门节点时，属性不可查看。
            if (this.toolStripButton4.Checked)//添加门节点
             {
                 return;
             }
             else if (this.toolStripButton5.Checked)//添加底节点
             {
                 return;

             }*/
            else
            {
                #region 显示事件窗体
                if (Snode.ItemType == "顶")//顶
                {
                    //MessageBox.Show(Snode.Name);
                    using (SetTopNode dlg = new SetTopNode(Snode))
                    {
                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            Snode = dlg.m_nodeInfo;
                            current.Tag = Snode;
                            CreateDoor(Snode.Type, current);
                            current.Text = dlg.textBoxJDMC.Text;
                        }
                    }

                }
                else if (Snode.ItemType == "门")//门
                {
                    //MessageBox.Show(Snode.Name);
                    using (SetDoorInfo dlg = new SetDoorInfo(Snode))
                    {
                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            Snode = dlg.m_nodeInfo;
                            current.Tag = Snode;
                            CreateDoor(Snode.Type, current);
                            current.Text = dlg.textBoxJDMC.Text;
                            rePaintTreeNode();
                        }
                    }

                }
                else if (Snode.ItemType == "底")//底
                {
                    //MessageBox.Show(Snode.Name);
                    using (SetBotNode dlg = new SetBotNode(Snode))
                    {
                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            Snode = dlg.m_nodeInfo;
                            current.Tag = Snode;
                            CreateBotNode(Snode.AffaType, current);
                            current.Text = dlg.textBoxJDMC.Text;
                        }
                    }

                }
                #endregion
            }
        }
        //单击画板
        /* private void diagramView1_Click(object sender, EventArgs e)
         {
             if (this.diagram1.ActiveItem == null)
                 return;
             SyswareNode current = (ShapeNode)this.diagram1.ActiveItem;
             NodeInfos Snode = current.Tag as NodeInfos;
             if (Snode == null)
                 if (Snode.ContainsNodes == null)
                     Snode.ContainsNodes = new NameCodeType.Collection();
             if (this.toolStripButton4.Checked)//添加门节点
             {
                 AddDoorNode(Snode, current);
             }
             else if (this.toolStripButton5.Checked)//添加底节点
             {
                 AddBotNode(Snode, current);
             }
             /*else
             {
                 #region 显示事件窗体
                 if (Snode.ItemType == "顶")//顶
                 {
                     //MessageBox.Show(Snode.Name);
                     using (SetTopNode dlg = new SetTopNode(Snode))
                     {
                         if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                         {
                             Snode = dlg.m_nodeInfo;
                             current.Tag = Snode;
                             CreateDoor(Snode.Type, current);
                         }
                     }

                 }
                 else if (Snode.ItemType == "门")//门
                 {
                     //MessageBox.Show(Snode.Name);
                     using (SetDoorInfo dlg = new SetDoorInfo(Snode))
                     {
                         if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                         {
                             Snode = dlg.m_nodeInfo;
                             current.Tag = Snode;
                             CreateDoor(Snode.Type, current);
                         }
                     }

                 }
                 else if (Snode.ItemType == "底")//底
                 {
                     //MessageBox.Show(Snode.Name);
                     using (SetBotNode dlg = new SetBotNode(Snode))
                     {
                         if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                         {
                             Snode = dlg.m_nodeInfo;
                             current.Tag = Snode;
                             CreateBotNode(Snode.AffaType, current);
                         }
                     }

                 }
                 #endregion
             }
         }*/
        private void diagramView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (this.diagram1.ActiveItem == null)
                    return;
                if (this.diagram1.ActiveItem.GetType().Name != "SyswareNode") return;

                SyswareNode current = (SyswareNode)this.diagram1.ActiveItem;
                if (current != null)
                    this.diagramView1.ContextMenuStrip = this.contextMenuStrip1;
                else this.diagramView1.ContextMenuStrip = null;

                PositionTest(current);
            }
        }
        private void PositionTest(SyswareNode current)
        {


            PointF c = current.GetCenter();
            RectangleF b = current.GetBounds();
            RectangleF lb = current.GetBounds();
            //RectangleF lb = current.GetLocalBounds();

            // MessageBox.Show(c.ToString() + "  " + b.ToString() + "  " + lb.ToString());



        }
        #endregion

        #region  右键复制粘贴
        private Dictionary<string, SyswareNode> m_stacks = null;
        //复制
        private void ToolStripMenuItemFZ_Click(object sender, EventArgs e)
        {
            CopyNodes();
        }
        //粘帖
        private void ToolStripMenuItemNT_Click(object sender, EventArgs e)
        {
            PasteNodes();

        }
        //特殊粘帖
        private void ToolStripMenuItemTSNT_Click(object sender, EventArgs e)
        {
            SpecialPasteNodes();
        }
        //剪切
        private void ToolStripMenuItemJQ2_Click(object sender, EventArgs e)
        {
            CutNodes();
            /*m_pasteDX = 0;
            m_pasteDY = 0;

            if (this.diagram1.ActiveItem == null) 
             * return;
            SyswareNode current = (ShapeNode)this.diagram1.ActiveItem;
            m_testSnode = current.Tag as NodeInfos;
            if (m_testSnode == null) 
             * return;
            this.diagramView1.CutToClipboard(true);*/
        }
        //删除
        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (this.diagram1.ActiveItem == null)
                return;

            Stack<DiagramItem> stacks = new Stack<DiagramItem>();
            stacks.Push(this.diagram1.ActiveItem);
            GetSubItems(this.diagram1.ActiveItem, stacks);

            while (stacks.Count > 0)
            {
                this.diagram1.Items.Remove(stacks.Pop());

            }
        }
        #endregion

        #region 工具栏 添加节点响应
        private void ToolStripMenuItemAddTop_Click(object sender, EventArgs e)
        {
            if (this.diagram1.ActiveItem == null)
                return;
            SyswareNode current = (SyswareNode)this.diagram1.ActiveItem;
            NodeInfos Snode = current.Tag as NodeInfos;
            if (Snode == null || Snode.ItemType == "底")
                return;
            AddDoorNode(Snode, current);
        }
        //添加底节点
        private void ToolStripMenuItemAddBot_Click(object sender, EventArgs e)
        {
            if (this.diagram1.ActiveItem == null)
                return;
            SyswareNode current = (SyswareNode)this.diagram1.ActiveItem;
            NodeInfos Snode = current.Tag as NodeInfos;
            if (Snode == null || Snode.ItemType == "底")
                return;
            AddBotNode(Snode, current);
        }
        #endregion

        #region 保存
        //保存xml文件,用于进行计算。
        private void xmlToolStripMenuItemSaveXML_Click(object sender, EventArgs e)
        {
            getXmlData();
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.OverwritePrompt = false;
                sfd.Title = "导出用于进行计算的XML文件";
                sfd.Filter = "文本文件(*.xml)|*.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        m_XmlDoc.Save(sfd.FileName);
                        MsgForm mf = new MsgForm("完成");
                        mf.ShowDialog();
                    }
                    catch (Exception ee)
                    {
                        //显示错误信息
                        MsgForm mf = new MsgForm(ee.Message);
                        mf.ShowDialog();
                    }
                }
            }
        }
        private void GetSubItems(SyswareNode topNode, XmlElement links, XmlElement nodes)
        {
            foreach (var link in this.diagram1.Links.Where(w => w.Origin == topNode))
            {
                SyswareNode next = (SyswareNode)link.Destination;
                NodeInfos nodeInfo = next.Tag as NodeInfos;
                if (nodeInfo != null)
                {
                    if (nodeInfo.ItemType == "门")
                    {
                        XmlElement field = m_XmlDoc.CreateElement("", "Gate", "");
                        field.SetAttribute("ID", nodeInfo.Code);
                        if (nodeInfo.ContainsNodes != null && nodeInfo.ContainsNodes.Count > 0)
                        {
                            string str = "Gate";
                            foreach (NameCodeType nct in nodeInfo.ContainsNodes)
                            {
                                if (nct.Type == "底") str = "Evt";
                                else str = "Gate";
                                XmlElement field1 = m_XmlDoc.CreateElement("", str, "");
                                field1.SetAttribute("ID", nct.Code);
                                field.AppendChild(field1);
                            }
                        }
                        links.AppendChild(field);

                        //节点
                        XmlElement gate = m_XmlDoc.CreateElement("", "Gate", "");
                        gate.SetAttribute("ID", nodeInfo.Code);
                        gate.SetAttribute("Logic", GetNodeType(nodeInfo.Type));
                        nodes.AppendChild(gate);
                    }
                    else if (nodeInfo.ItemType == "底")
                    {
                        XmlElement gate = m_XmlDoc.CreateElement("", "Evt", "");
                        gate.SetAttribute("ID", nodeInfo.Code);
                        if (nodeInfo.AffaType == AffairType.房型事件)
                        {
                            string value = "";
                            if (nodeInfo.Mxlj == "True")
                                value = "true";
                            else if (nodeInfo.Mxlj == "False")
                                value = "false";
                            gate.SetAttribute("Lmd", value);
                        }
                        else if (nodeInfo.AffaType == AffairType.隐蔽事件)
                        {
                            gate.SetAttribute("Lmd", nodeInfo.Gzl);
                            gate.SetAttribute("Ti", nodeInfo.Jszq);
                        }
                        else gate.SetAttribute("Lmd", nodeInfo.Gzl);

                        nodes.AppendChild(gate);
                    }
                }
                GetSubItems(next, links, nodes);
            }
        }

        private void GetSubItems(SyswareNode current, Stack<SyswareNode> stacks)
        {
            foreach (var link in this.diagram1.Links.Where(w => w.Origin == current))
            {
                SyswareNode next = (SyswareNode)link.Destination;
                stacks.Push(next);
                GetSubItems(next, stacks);
            }
        }

        private string GetNodeType(NodeType type)
        {
            string str = "";
            switch (type)
            {
                case NodeType.与门:
                    str = "AND";
                    break;
                case NodeType.或门:
                    str = "OR";
                    break;
                case NodeType.禁止门:
                    str = "INH";
                    break;
                case NodeType.优先与门:
                    str = "PAND";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }
        #endregion

        #region 退出/计算结果
        //退出
        private void ToolStripMenuItemColse_Click(object sender, EventArgs e)
        {
            ExitConfirmForm ecf = new ExitConfirmForm();
            if (ecf.ShowDialog() == DialogResult.Cancel)
                return;
            //while (MessageBox.Show("退出当前窗体？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    System.Environment.Exit(System.Environment.ExitCode);
            try
            {
                this.diagram1.ClearAll();
                //System.Environment.Exit(System.Environment.ExitCode);
                this.Close();
            }
            catch { }
        }

        private void 全部计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MsgForm mf;
            if (this.diagram1.Nodes.Count() == 0)
            {
                mf = new MsgForm("没有节点需要计算");
            }
            else
            {
                mf = new MsgForm("计算完成");
            }

            mf.Show();
        }
        //计算结果
        private void 查看结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getXmlData();
            string sourceXml = m_XmlDoc.InnerXml;
            string resultCutSet = getResultCutSet();
            double topProbability = getTopProbability();
            string bottomImportance = getBottomImportance();
            List<ProResult> prList = getNodes();
            View_Results vr = new View_Results();

            vr.Show();
            vr.setData(resultCutSet, topProbability, bottomImportance, prList);
        }
        //"关于"菜单
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("SyswareFlowChartTest  版本v0.3  (C)2017 Sysware company    保留所有权利.");
            AboutForm af = new AboutForm();
            af.ShowDialog();
        }

        private void 在线帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /* private void toolStripMenuItem1_Click(object sender, EventArgs e)
         {
             string name = Application.StartupPath + "\\Test\\test.psa";
             if (File.Exists(name))
                 File.Delete(name);
             //this.diagramView1.SaveToFile(name, false);
             Stream stre = new FileStream(name, FileMode.OpenOrCreate);

             this.diagramView1.SaveToStream(stre, true);
             MessageBox.Show("打印完成！存放路径" + name, "提示");
             stre.Close();
             stre.Dispose();
         }*/

        private List<ProResult> getNodes()
        {
            List<ProResult> prList = new List<ProResult>();
            for (int i = 0; i < this.diagram1.Nodes.Count; i++)
            {
                SyswareNode node = (SyswareNode)this.diagram1.Nodes[i];
                NodeInfos nodeInfo = node.Tag as NodeInfos;
                ProResult pr = new ProResult();
                pr.Name = nodeInfo.Name;
                pr.Code = nodeInfo.Code;
                pr.Type = nodeInfo.Type.ToString();
                pr.Fpgl = nodeInfo.Fpgl;
                prList.Add(pr);
            }
            return prList;
        }
        #endregion

        #region 投标
        private void getXmlData()
        {
            if (m_XmlDoc == null)
            {
                m_XmlDoc = new XmlDocument();
            }
            else
            {
                m_XmlDoc.RemoveAll();
            }

            if (this.diagram1.Nodes.Count() == 0)
            {
                return;
            }
            //XmlNode xmlnode = m_XmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            //xmlnode.InnerText += " encoding=\"gb2312\"";
            //m_XmlDoc.AppendChild(xmlnode);
            //加入一个根元素
            XmlElement xmlelem = m_XmlDoc.CreateElement("", "FTProject", "");
            xmlelem.SetAttribute("ID", "FT");
            xmlelem.SetAttribute("FTime", "5");
            m_XmlDoc.AppendChild(xmlelem);

            XmlNode database = m_XmlDoc.SelectSingleNode("FTProject");

            XmlElement links = m_XmlDoc.CreateElement("", "Links", "");
            XmlElement nodes = m_XmlDoc.CreateElement("", "Nodes", "");
            //table.SetAttribute("code", dr[1].ToString());
            //table.SetAttribute("name", dr[0].ToString());
            SyswareNode topNode = (SyswareNode)this.diagram1.Nodes[0];
            NodeInfos topNodeInfo = topNode.Tag as NodeInfos;

            if (topNodeInfo != null)
            {
                XmlElement gate = m_XmlDoc.CreateElement("", "Gate", "");
                gate.SetAttribute("ID", topNodeInfo.Code);
                if (topNodeInfo.ContainsNodes != null && topNodeInfo.ContainsNodes.Count > 0)
                {
                    string str = "Gate";
                    foreach (NameCodeType nct in topNodeInfo.ContainsNodes)
                    {
                        if (nct.Type == "底") str = "Evt";
                        else str = "Gate";
                        XmlElement gate1 = m_XmlDoc.CreateElement("", str, "");
                        gate1.SetAttribute("ID", nct.Code);
                        gate.AppendChild(gate1);
                    }
                }
                links.AppendChild(gate);

                // Top节点的数据
                XmlElement topgate = m_XmlDoc.CreateElement("", "Gate", "");
                topgate.SetAttribute("ID", topNodeInfo.Code);
                topgate.SetAttribute("Logic", GetNodeType(topNodeInfo.Type));
                nodes.AppendChild(topgate);
            }
            else
            {
                return;
            }

            GetSubItems((SyswareNode)this.diagram1.Nodes[0], links, nodes);
            database.AppendChild(links);
            database.AppendChild(nodes);

        }

        /*NEntrance go;*/
        //割集计算结果
        private string getResultCutSet()
        {
            getXmlData();
            string text = (new CutsetEntrance()).GO(m_XmlDoc.InnerXml);
            return text;
        }

        //计算顶事件发生概率
        private double getTopProbability()
        {
            getXmlData();
            FTBNEntrance go = new FTBNEntrance(m_XmlDoc.InnerXml, true);
            go.GOCalculateProbability();
            double text = go.GetPrbResult();
            return text;
        }

        // 计算底事件重要度
        private string getBottomImportance()
        {
            getXmlData();
            FTBNEntrance go = new FTBNEntrance(m_XmlDoc.InnerXml, true);
            go.GOCalculateProbability();
            go.GOCalculateImportance();
            string text = go.GetIMPsResultXML();

            return text;
        }
        #endregion

        #region 树

        private void initTreeView()
        {
            ImageList il = new ImageList();
            Image image = Properties.Resources.triangle;
            il.Images.Add(image);
            this.treeView1.ImageList = il;
        }
        private void createTopNode()
        {
            //新建top节点时，先将所有节点删除
            treeView1.Nodes.Clear();
            TreeNode tNode = new TreeNode();
            tNode.Text = TopCode;
            tNode.Name = TopCode;
            tNode.ImageIndex = tNode.SelectedImageIndex = 1;
            treeView1.Nodes.Add(tNode);
        }
        /// <summary>
        /// 绑定TreeView（利用TreeNode）
        /// </summary>
        /// <param name="p_Node">TreeNode（TreeView的一个节点）</param>
        /// <param name="pid_val">父id的值</param>
        /// <param name="id">数据库 id 字段名</param>
        /// <param name="pid">数据库 父id 字段名</param>
        /// <param name="text">数据库 文本 字段值</param>
        protected void Bind_Tv(DataTable dt, TreeNode p_Node, string pid_val, string id, string pid, string text)
        {
            DataView dv = new DataView(dt);
            TreeNode tn;//建立TreeView的节点，以便将取出的数据添加到节点中
            string filter = string.IsNullOrEmpty(pid_val) ? pid + " is null" : string.Format(pid + "='{0}'", pid_val);
            dv.RowFilter = filter;//利用DataView将数据进行筛选，选出相同 父id值 的数据
            foreach (DataRowView row in dv)
            {
                tn = new TreeNode();
                if (p_Node == null)//如果为根节点
                {
                    tn.Name = row[id].ToString();
                    tn.Text = row[text].ToString();
                    treeView1.Nodes.Add(tn);//将该节点加入到TreeView中
                    Bind_Tv(dt, tn, tn.Name, id, pid, text);//递归（反复调用这个方法，直到把数据取完为止）
                }
                else//如果不是根节点
                {
                    tn.Name = row[id].ToString();
                    tn.Text = row[text].ToString();
                    p_Node.Nodes.Add(tn);//该节点加入到上级节点中
                    Bind_Tv(dt, tn, tn.Name, id, pid, text);//递归
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            //循环找到点击树结构节点对应的SyswareNode
            SyswareNode currentSNode = new SyswareNode();
            for (int i = 0; i < this.diagram1.Nodes.Count; i++)
            {
                SyswareNode node = (SyswareNode)this.diagram1.Nodes[i];
                NodeInfos nodeInfo = node.Tag as NodeInfos;
                NodeInfos pnodeInfo = node.Parent.Tag as NodeInfos;
                if (nodeInfo.Code == e.Node.Name)
                {
                    currentSNode = node;
                }

            }
            //通过点击的节点，找到所有子节点
            Stack<SyswareNode> stacks = new Stack<SyswareNode>();
            if (currentSNode == null)
            {
                return;
            }
            else
            {
                stacks.Push(currentSNode);
                GetSubItems(currentSNode, stacks);
            }

            //先将所有节点和节点之间的连接线隐藏
            for (int i = 0; i < this.diagram1.Nodes.Count; i++)
            {
                SyswareNode node = (SyswareNode)this.diagram1.Nodes[i];
                node.Visible = false;
            }
            for (int i = 0; i < this.diagram1.Links.Count; i++)
            {
                DiagramLink link = this.diagram1.Links[i];
                link.Visible = false;
            }
            //再将选中的节点进行展示
            foreach (SyswareNode node2 in stacks)
            {
                NodeInfos nodeInfo2 = node2.Tag as NodeInfos;
                for (int i = 0; i < this.diagram1.Nodes.Count; i++)
                {
                    SyswareNode node = (SyswareNode)this.diagram1.Nodes[i];
                    NodeInfos nodeInfo = node.Tag as NodeInfos;

                    if (nodeInfo.Code == nodeInfo2.Code)
                    {
                        node2.Visible = true;
                        for (int j = 0; j < node2.OutgoingLinks.Count; j++)
                        {
                            node2.OutgoingLinks[j].Visible = true;
                        }

                    }
                }
            }
            this.diagramView1.ZoomToFit();
        }

        private void rePaintTreeNode()
        {
            this.treeView1.Nodes.Clear();
            List<SyswareTreeNode> sysNodes = new List<SyswareTreeNode>();
            foreach (SyswareNode i in this.diagram1.Nodes)
            {
                NodeInfos ni = i.Tag as NodeInfos;

                SyswareTreeNode sysNode = new SyswareTreeNode();
                sysNode.code = sysNode.name = ni.Code;
                sysNode.text = ni.Name;
                sysNode.pCode = sysNode.pName = ni.ParentCode == null ? "-1" : ni.ParentCode;
                sysNode.isPager = ni.isPager;
                sysNodes.Add(sysNode);

            }
            DataTable sourceDt = Data2Conversion.List2DataTable<SyswareTreeNode>(sysNodes);
            string filter = "isPager = 'false' ";
            DataTable reduceDt = changeDt(sourceDt, filter);
            DataTable newDt = getTreeDt(sourceDt, reduceDt);
            Bind_Tv(newDt, null, "-1", "code", "pCode", "text");
            treeView1.Refresh();
            treeView1.ExpandAll();
        }
        /// <summary>
        /// 对DataTable进行筛选返回新的dt
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private DataTable changeDt(DataTable dt, string filter)
        {
            DataView dv = new DataView(dt);//将DataTable存到DataView中，以便于筛选数据
            dv.RowFilter = filter;//利用DataView将数据进行筛选，选出相同 父id值 的数据
            DataTable newTable = dv.ToTable();
            return newTable;
        }
        /// <summary>
        /// 将获得到的节点树进行修改，改为分页节点树
        /// </summary>
        /// <param name="sourceDt">源数据</param>
        /// <param name="reduceDt">不是分页的数据</param>
        /// <returns></returns>
        private DataTable getTreeDt(DataTable sourceDt, DataTable reduceDt)
        {
            DataTable newDt = new DataTable();
            //先反着循环
            for (int i = reduceDt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = reduceDt.Rows[i];
                foreach (DataRow sdr in sourceDt.Rows)
                {
                    if (sdr["pCode"] == dr["code"])
                    {
                        sdr["pCode"] = dr["pCode"];
                        sdr["pName"] = dr["pName"];
                    }
                }
            }
            //两次循环确保所有父节点不存在不是分页的节点
            foreach (DataRow dr in reduceDt.Rows)
            {
                foreach (DataRow sdr in sourceDt.Rows)
                {
                    if (sdr["pCode"] == dr["code"])
                    {
                        sdr["pCode"] = dr["pCode"];
                        sdr["pName"] = dr["pName"];
                    }
                }
            }
            string filter = "isPager = 'true' ";
            newDt = changeDt(sourceDt, filter);
            return newDt;
        }

        #endregion
    }
}

