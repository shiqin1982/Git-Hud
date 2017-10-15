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

namespace SyswareFlowChartTest
{
    public partial class MainPage : Form
    {
        private RectangleF m_nodeBounds = RectangleF.Empty;
        private int m_doorSequ = 1;
        private int m_botSequ = 1;
        //private int m_pasteDX = 0;
        //private int m_pasteDY =0;
        private string m_testSnode = null;//临时节点编码信息（复杂）
        private XmlDocument m_XmlDoc = null;

        public MainPage()
        {
            InitializeComponent();
            m_nodeBounds = new RectangleF(10, 0, 25, 25);
        }
        private void MainPage_Load(object sender, EventArgs e)
        {
            this.diagramView1.Behavior = Behavior.DrawLinks;
            this.diagram1.NodeEffects.Add(new GlassEffect());
            this.diagram1.NodeEffects.Add(new AeroEffect());

            Rectangle rect = new Rectangle(0, 0, 75, 75);
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
        /// <summary>
        /// 创建顶节点
        /// </summary>
        private void CreateBaseNode()
        {
            this.diagram1.ClearAll();
            RectangleF nodeBounds = new RectangleF(50, 20, 25, 25);
            ShapeNode root = this.diagram1.Factory.CreateShapeNode(nodeBounds);
            CreateDoor(NodeType.与门, root);
            root.Text = "Top";
            Thickness tk = new Thickness(0, 11, 0, 0);
            root.TextPadding = tk;
            NodeInfos nodeInfo = new NodeInfos("T");
            nodeInfo.Name = "顶事件";
            nodeInfo.ItemType = "顶";
            nodeInfo.Type = NodeType.与门;
            nodeInfo.isPager = true;
            root.Tag = nodeInfo;
            UserLayOut();
            createTopNode();
            //this.diagramView1
        }
        //添加、新建门节点
        private void AddDoorNode(NodeInfos selectRootNodeTag, ShapeNode selectRootNode)
        {
            if (selectRootNodeTag.ItemType == "底")
                return;
            ShapeNode node = this.diagram1.Factory.CreateShapeNode(m_nodeBounds);
            CreateDoor(NodeType.与门, node);

            NodeInfos nodeInfo = new NodeInfos("G" + m_doorSequ.ToString());
            nodeInfo.Name = "逻辑门事件";
            nodeInfo.ItemType = "门";
            nodeInfo.Type = NodeType.与门;
            nodeInfo.ParentCode = selectRootNodeTag.Code;
            nodeInfo.ContainsNodes = new NameCodeType.Collection();
            node.Tag = nodeInfo;
            node.Text = "GATE" + m_doorSequ.ToString();
            Thickness tk = new Thickness(0, 11, 0, 0);
            node.TextPadding = tk;
            //if (Snode.ContainsNodes.Contains();
            //selectRootNodeTag.ContainsNodes.Add(new NameCodeType(nodeInfo.Code, nodeInfo.ItemType));
            this.diagram1.Factory.CreateDiagramLink(selectRootNode, node);
            UserLayOut();
            m_doorSequ++;
            //勾选分页则创建
            if (nodeInfo.isPager)
            {
                createDoorNode(node);
            }

        }
        /// <summary>
        /// 创建门节点样式
        /// </summary>
        /// <param name="current"></param>
        private void CreateDoor(NodeType type, ShapeNode node)
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
        private void CreateAndDoorStyle(ShapeNode node)
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
        private void CreateOrDoorStyle(ShapeNode node)
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
        private void CreateBanDoorStyle(ShapeNode node)
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
        private void CreatePrioAndDoorStyle(ShapeNode node)
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
        private void CreatePagingStyle(ShapeNode node)
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
        private void AddBotNode(NodeInfos selectRootNodeTag, ShapeNode selectRootNode)
        {
            if (selectRootNodeTag.ItemType == "底")
                return;
            ShapeNode node = this.diagram1.Factory.CreateShapeNode(m_nodeBounds);
            CreateBotNode(AffairType.基本事件, node);
            NodeInfos nodeInfo = new NodeInfos("E" + m_botSequ.ToString());
            nodeInfo.Name = "事件";
            nodeInfo.ItemType = "底";
            nodeInfo.AffaType = AffairType.基本事件;
            nodeInfo.ContainsNodes = new NameCodeType.Collection();
            node.Tag = nodeInfo;
            node.Text = "EVENT" + m_botSequ.ToString();
            Thickness tk = new Thickness(0, 11, 0, 0);
            node.TextPadding = tk;
            //if (!Snode.ContainsNodes.Contains(nodeInfo.Code))
            selectRootNodeTag.ContainsNodes.Add(new NameCodeType(nodeInfo.Code, nodeInfo.ItemType));
            this.diagram1.Factory.CreateDiagramLink(selectRootNode, node);
            UserLayOut();
            m_botSequ++;
        }
        /// <summary>
        /// 创建底节点
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ShapeNode CreateBotNode(AffairType type, ShapeNode node)
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
        private void CreateBaseAffairStyle(ShapeNode node)
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
				new LineTemplate(20, 80, 20, 60),
				new BezierTemplate(20, 60, 40, 40, 60f, 40, 80, 60),
				new LineTemplate(80, 60, 80, 80),
				new LineTemplate(80, 80, 80, 90),
				new BezierTemplate(80, 90, 60, 100, 40f, 100, 20, 90),
				new LineTemplate(20, 90, 20, 80)
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
        /// 条件事件样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateFiletAffairStyle(ShapeNode node)
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
		)
	}
);
        }
        /// <summary>
        /// 未展开事件样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateUnfoldAffairStyle(ShapeNode node)
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
				new LineTemplate(20, 70, 50, 50),
				//new LineTemplate(50, 50, 80, 70),
				//new LineTemplate(80, 70, 80, 80),
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
		)
	}
);

        }
        /// <summary>
        /// 房型事件样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateRoomAffairStyle(ShapeNode node)
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
				new LineTemplate(0, 40, 0, 20),
				new LineTemplate (0,55,10,55),
				new LineTemplate(10,55,10,45),
				new LineTemplate(10,45,0,45),
				new LineTemplate(0,45,0,55)
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
		)
	}
);

        }
        /// <summary>
        /// 隐蔽事件样式
        /// </summary>
        /// <param name="node"></param>
        private void CreateHideAffairStyle(ShapeNode node)
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
				new LineTemplate(20, 70, 50, 40),
				new LineTemplate(50, 40, 80, 70),
				new LineTemplate(80, 70, 50, 100),
				new LineTemplate(50, 100, 20, 70)
			},
			new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 128, 128, 255)),
			FillMode.Winding,
			new MindFusion.Drawing.Pen(Color.FromName("Black"), 0.1f)
		),new ShapeDecoration(	
			new ElementTemplate[]
			{
				new LineTemplate(50, 50, 70, 70),
				new LineTemplate(70, 70, 50, 90),
				new LineTemplate(50, 90, 30, 70),
				new LineTemplate(30, 70, 50, 50)
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

        //private void RecursionSaveNode(TreeNode tn)
        //{
        //    foreach (TreeNode tnSub in tn.Nodes)
        //    {
        //        m_SubNode.Add(tnSub.Text); //注意这个位置加上Text，如果写成tnSub.ToString(),那存储的就不是名字了，而是"TreeNode:名字",多了一个TreeNode:字符  
        //        RecursionSaveNode(tnSub);
        //    }
        //} 

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
                code += m_doorSequ.ToString();
                m_doorSequ++;
            }
            else
            {
                code += m_botSequ.ToString();
                m_botSequ++;
            }
            return code;
        }
        private NodeInfos UpdataNodeInfo(ShapeNode nnode)
        {
            NodeInfos oldNode = nnode.Tag as NodeInfos;
            NodeInfos newNode = new NodeInfos(oldNode.Code);
            newNode.AffaType = oldNode.AffaType;
            newNode.ContainsNodes = oldNode.ContainsNodes;
            newNode.Fpgl = oldNode.Fpgl;
            newNode.Fxxs = oldNode.Fxxs;
            newNode.Glms = oldNode.Glms;
            newNode.Gzl = oldNode.Gzl;
            newNode.ItemType = oldNode.ItemType;
            newNode.Jdms = oldNode.Jdms;
            newNode.Jszq = oldNode.Jszq;
            newNode.Name = oldNode.Name;
            newNode.ParentCode = oldNode.ParentCode;
            newNode.Pjsxgl = oldNode.Pjsxgl;
            newNode.Type = oldNode.Type;

            return newNode;
        }
        private void CopyNodes()
        {
            if (this.diagram1.ActiveItem == null)
                return;
            m_stacks = new Dictionary<string, ShapeNode>();
            ShapeNode seleNode = (ShapeNode)this.diagram1.ActiveItem;
            NodeInfos nodei = seleNode.Tag as NodeInfos;
            m_testSnode = nodei.Code.Clone().ToString();

            ShapeNode nnode = seleNode.Clone(true) as ShapeNode;
            nnode.Tag = UpdataNodeInfo(nnode);
            nnode.Bounds = new RectangleF(seleNode.Bounds.X + 10, seleNode.Bounds.Y + 10, seleNode.Bounds.Width, seleNode.Bounds.Height);

            m_stacks.Add(nodei.Code, nnode);

            GetSubItems((ShapeNode)this.diagram1.ActiveItem, true);
            /*m_pasteDX = 10;
            m_pasteDY = 10;

            if (this.diagram1.ActiveItem == null) return;
            if (this.diagram1.ActiveItem is ShapeNode)
            {
                ShapeNode current = (ShapeNode)this.diagram1.ActiveItem;
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
                MessageBox.Show("请先复制！", "提示");
                return;
            }

            ShapeNode currentSele = (ShapeNode)this.diagram1.ActiveItem;

            foreach (var stack in m_stacks)
            {
                this.diagram1.Nodes.Add(stack.Value);
            }

            ShapeNode oneCurrentSele = m_stacks[m_testSnode];
            this.diagram1.Links.Add(new DiagramLink(this.diagram1, currentSele, oneCurrentSele));

            SetNodeLine();
            UserLayOut();
            m_stacks = null;
            /*ShapeNode current1 = (ShapeNode)this.diagram1.ActiveItem;

            this.diagramView1.PasteFromClipboard(m_pasteDX, m_pasteDY, true);



            if (this.diagram1.ActiveItem == null) 
             * return;
			
            if (this.diagram1.ActiveItem is ShapeNode)
            {
                ShapeNode current = (ShapeNode)this.diagram1.ActiveItem;
                current.Tag = m_testSnode;
            }
            else if (this.diagram1.ActiveItem is DiagramLink)
            {
                DiagramLink dia = this.diagram1.ActiveItem as DiagramLink;
                dia.
            }
            m_pasteDX += 10;
            m_pasteDY += 10;

            ShapeNode current2 = (ShapeNode)this.diagram1.ActiveItem;

            this.diagram1.Links.Add(new DiagramLink(this.diagram1, current1, current2));*/
        }
        /// <summary>
        ///特殊粘帖 
        /// </summary>
        private void SpecialPasteNodes()
        {
            if (this.diagram1.ActiveItem == null || m_stacks == null)
            {
                MessageBox.Show("请先复制！", "提示");
                return;
            }

            ShapeNode currentSele = (ShapeNode)this.diagram1.ActiveItem;

            NodeInfos test = null;
            foreach (var stack in m_stacks)
            {
                test = stack.Value.Tag as NodeInfos;
                test.Code = SetCopyNodeCode(test);
                this.diagram1.Nodes.Add(stack.Value);
            }

            ShapeNode oneCurrentSele = m_stacks[m_testSnode];
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
            m_stacks = new Dictionary<string, ShapeNode>();
            ShapeNode seleNode = (ShapeNode)this.diagram1.ActiveItem;
            NodeInfos nodei = seleNode.Tag as NodeInfos;
            m_testSnode = nodei.Code.Clone().ToString();

            //ShapeNode nnode = (ShapeNode)seleNode.Clone(true);
            //nnode.Bounds = new RectangleF(seleNode.Bounds.X + 10, seleNode.Bounds.Y + 10, seleNode.Bounds.Width, seleNode.Bounds.Height);

            m_stacks.Add(nodei.Code, seleNode);

            GetSubItems((ShapeNode)this.diagram1.ActiveItem, false);

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
        private void GetSubItems(ShapeNode current, bool isCopy)
        {
            foreach (var link in this.diagram1.Links.Where(w => w.Origin == current))
            {
                ShapeNode next = (ShapeNode)link.Destination;
                NodeInfos nodei = next.Tag as NodeInfos;
                if (isCopy)
                {
                    ShapeNode nshape = (ShapeNode)next.Clone(true);
                    nshape.Bounds = new RectangleF(next.Bounds.X + 10, next.Bounds.Y + 10, next.Bounds.Width, next.Bounds.Height);
                    nshape.Tag = UpdataNodeInfo(nshape);
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
        private void OpenFileRefresh(ShapeNode topNode)
        {
            foreach (var link in this.diagram1.Links.Where(w => w.Origin == topNode))
            {
                ShapeNode next = (ShapeNode)link.Destination;
                NodeInfos nodeInfo = next.Tag as NodeInfos;
                if (nodeInfo.ItemType == "底")
                    CreateBotNode(nodeInfo.AffaType, next);
                else
                    CreateDoor(nodeInfo.Type, next);

                OpenFileRefresh(next);
            }
        }

        //新建
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.diagram1.ActiveItem == null)
            {
                this.diagramView1.Behavior = Behavior.DrawLinks;
                this.diagram1.NodeEffects.Add(new GlassEffect());
                this.diagram1.NodeEffects.Add(new AeroEffect());

                m_doorSequ = 1;
                m_botSequ = 1;
                CreateBaseNode();
            }

            else
            {
                DialogResult dr = MessageBox.Show("新建后当前文件将被覆盖！", "提示", MessageBoxButtons.OKCancel);
                {
                    if (dr == DialogResult.OK)
                    {
                        this.diagramView1.Behavior = Behavior.DrawLinks;
                        this.diagram1.NodeEffects.Add(new GlassEffect());
                        this.diagram1.NodeEffects.Add(new AeroEffect());

                        m_doorSequ = 1;
                        m_botSequ = 1;
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
                //dlg.Filter = "psa文件 (*.psa)|*.psa|所有文件 (*.*)|*.*";
                dlg.Filter = "psa文件 (*.psa)|*.psa";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.diagram1.ClearAll();

                    Stream stre = new FileStream(dlg.FileName, FileMode.Open);

                    this.diagramView1.LoadFromStream(stre);// LoadFromFile(dlg.FileName);//打开文件"E:\\杨稳\\11.psa"
                    stre.Close();
                    stre.Dispose();

                    ShapeNode top = (ShapeNode)this.diagram1.Nodes[0];
                    NodeInfos nodeinfo = top.Tag as NodeInfos;
                    if (nodeinfo != null)
                    {
                        CreateDoor(nodeinfo.Type, top);
                        OpenFileRefresh(top);
                    }
                }
            }
        }
        //保存
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string name = Application.StartupPath + "\\Test\\test.psa";
            if (File.Exists(name))
                File.Delete(name);
            //this.diagramView1.SaveToFile(name, false);
            Stream stre = new FileStream(name, FileMode.OpenOrCreate);

            this.diagramView1.SaveToStream(stre, true);
            MessageBox.Show("保存完成！存放路径" + name, "提示");
            stre.Close();
            stre.Dispose();
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
        //另存为
        private void ToolStripMenuItemSaveTo_Click(object sender, EventArgs e)
        {
            //this.diagramView.SaveToFile("E:\\杨稳\\11.psa", true);//另存为文件
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "psa文件 (*.psa)|*.psa";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.diagramView1.SaveToFile(dlg.FileName, false);
                }
            }
        }
        //打印(生成xml文档)
        private void toolStripButton13_Click_1(object sender, EventArgs e)
        {
            string name = Application.StartupPath + "\\Test\\test.psa";
            if (File.Exists(name))
                File.Delete(name);
            //this.diagramView1.SaveToFile(name, false);
            Stream stre = new FileStream(name, FileMode.OpenOrCreate);

            this.diagramView1.SaveToStream(stre, true);
            MessageBox.Show("生成文档完成！存放路径" + name, "提示");
            stre.Close();
            stre.Dispose();
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
        //满屏
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            this.diagramView1.ZoomToFit();
        }
        //双击画板
        private void diagramView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.diagram1.ActiveItem == null)
                return;
            ShapeNode current = (ShapeNode)this.diagram1.ActiveItem;
            NodeInfos Snode = current.Tag as NodeInfos;
            if (Snode == null)
                return;
            if (Snode.ContainsNodes == null)
                Snode.ContainsNodes = new NameCodeType.Collection();
            //创建门节点时，属性不可查看。
            /*if (this.toolStripButton4.Checked)//添加门节点
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
                            ShapeNode node = this.diagram1.Factory.CreateShapeNode(m_nodeBounds);
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
                            if (dlg.m_nodeInfo.isPager)
                            {
                                createDoorNode(current);
                            }
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
        }
        //单击画板
        private void diagramView1_Click(object sender, EventArgs e)
        {
            if (this.diagram1.ActiveItem == null)
                return;
            ShapeNode current = (ShapeNode)this.diagram1.ActiveItem;
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
            }*/
        }
        private void diagramView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (this.diagram1.ActiveItem == null)
                    return;
                ShapeNode current = (ShapeNode)this.diagram1.ActiveItem;
                if (current != null)
                    this.diagramView1.ContextMenuStrip = this.contextMenuStrip1;
                else this.diagramView1.ContextMenuStrip = null;
            }
        }
        private Dictionary<string, ShapeNode> m_stacks = null;
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
            ShapeNode current = (ShapeNode)this.diagram1.ActiveItem;
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
        //添加门节点
        private void ToolStripMenuItemAddTop_Click(object sender, EventArgs e)
        {
            if (this.diagram1.ActiveItem == null)
                return;
            ShapeNode current = (ShapeNode)this.diagram1.ActiveItem;
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
            ShapeNode current = (ShapeNode)this.diagram1.ActiveItem;
            NodeInfos Snode = current.Tag as NodeInfos;
            if (Snode == null || Snode.ItemType == "底")
                return;
            AddBotNode(Snode, current);
        }
        //保存xml文件
        private void xmlToolStripMenuItemSaveXML_Click(object sender, EventArgs e)
        {
            m_XmlDoc = new XmlDocument();
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
            ShapeNode topNode = (ShapeNode)this.diagram1.Nodes[0];
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
            }
            else
                return;

            GetSubItems((ShapeNode)this.diagram1.Nodes[0], links, nodes);
            database.AppendChild(links);
            database.AppendChild(nodes);
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "文本文件(*.xml)|*.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        m_XmlDoc.Save(sfd.FileName);
                        MessageBox.Show("完成");
                    }
                    catch (Exception ee)
                    {
                        //显示错误信息
                        MessageBox.Show(ee.Message);
                    }
                }
            }
        }
        private void GetSubItems(ShapeNode topNode, XmlElement links, XmlElement nodes)
        {
            foreach (var link in this.diagram1.Links.Where(w => w.Origin == topNode))
            {
                ShapeNode next = (ShapeNode)link.Destination;
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
                                value = "1";
                            else if (nodeInfo.Mxlj == "False")
                                value = "0";
                            gate.SetAttribute("Val", value);
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

        private void GetSubItems(ShapeNode current, Stack<ShapeNode> stacks)
        {
            foreach (var link in this.diagram1.Links.Where(w => w.Origin == current))
            {
                ShapeNode next = (ShapeNode)link.Destination;
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

        //退出
        private void ToolStripMenuItemColse_Click(object sender, EventArgs e)
        {
            while (MessageBox.Show("退出当前窗体？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                System.Environment.Exit(System.Environment.ExitCode);

        }
        //计算结果
        private void 查看结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string resultCutSet = getResultCutSet();
            string topProbability = getTopProbability();
            string bottomImportance = getBottomImportance();

            new View_Results().Show();
        }
        //"关于"菜单
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SyswareFlowChartTest  版本v0.3  (C)2017 Sysware company    保留所有权利.");
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

        ///<summary>
        ///分页功能
        ///</summary>
        private void checkBoxFY_Click(object sender, EventArgs e)
        {
            this.diagramView1.Behavior = Behavior.DrawLinks;
            this.diagram1.NodeEffects.Add(new GlassEffect());
            this.diagram1.NodeEffects.Add(new AeroEffect());

            m_doorSequ = 1;
            m_botSequ = 1;
            CreateBaseNode();

        }

        private string getXmlData()
        {
            m_XmlDoc = new XmlDocument();
            //加入一个根元素
            XmlElement xmlelem = m_XmlDoc.CreateElement("", "FTProject", "");
            xmlelem.SetAttribute("ID", "FT");
            xmlelem.SetAttribute("FTime", "5");
            m_XmlDoc.AppendChild(xmlelem);

            XmlNode database = m_XmlDoc.SelectSingleNode("FTProject");

            XmlElement links = m_XmlDoc.CreateElement("", "Links", "");
            XmlElement nodes = m_XmlDoc.CreateElement("", "Nodes", "");
            ShapeNode topNode = (ShapeNode)this.diagram1.Nodes[0];
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
            }
            else
            {
            }

            GetSubItems((ShapeNode)this.diagram1.Nodes[0], links, nodes);
            database.AppendChild(links);
            database.AppendChild(nodes);

            return m_XmlDoc.InnerXml.Replace("<Gate ID=\"T\" />", "");
        }

        /*NEntrance go;*/
        //割集计算结果
        private string getResultCutSet()
        {
            string text = "割集计算结果：" + Environment.NewLine + (new CutsetEntrance()).GO(getXmlData());
            return text;
        }

        //计算顶事件发生概率
        private string getTopProbability()
        {
            FTBNEntrance go = new FTBNEntrance(getXmlData(), true);
            go.GOCalculateProbability();
            string text = "顶事件发生概率：" + Environment.NewLine + go.GetPrbResult();
            return text;
        }

        // 计算底事件重要度
        private string getBottomImportance()
        {
            FTBNEntrance go = new FTBNEntrance(getXmlData(), true);
            go.GOCalculateProbability();
            go.GOCalculateImportance();
            string text = "重要度计算结果：" + Environment.NewLine + go.GetIMPsResultXML();
            return text;
        }

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
            tNode.Text = "Top";
            tNode.Name = "T";
            tNode.ImageIndex = tNode.SelectedImageIndex = 1;
            treeView1.Nodes.Add(tNode);
        }
        /// <summary>
        /// 创建一个分页树节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="node">对应的shapeNode</param>
        private void createDoorNode(ShapeNode node)
        {
            TreeNode tNode = new TreeNode();
            tNode.ImageIndex = tNode.SelectedImageIndex = 1;
            tNode.Text = node.Text;
            tNode.Name = ((SyswareFlowChartTest.NodeInfos)(node.Tag)).Code;
            
            string parentNodeCode = GetParentCode(node);
            TreeNode[] td = treeView1.Nodes.Find(parentNodeCode, true);
            if (td.Length == 1)
            {
                td[0].Nodes.Add(tNode);
                treeView1.SelectedNode = tNode;
            }
            else
            {
                td = treeView1.Nodes.Find("T", true);
                td[0].Nodes.Add(tNode);
                treeView1.SelectedNode = tNode;
            }

        }
        /// <summary>
        /// 根据传入节点返回是分页的父节点
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string GetParentCode(ShapeNode current)
        {
            NodeInfos nis = current.Tag as NodeInfos;
            string parentCode = nis.ParentCode;
            ShapeNode parentNode = new ShapeNode();          
            bool isPager = false;
            while (!isPager)
            {
                foreach (ShapeNode n in this.diagram1.Nodes.Where(i => ((NodeInfos)i.Tag).Code == parentCode))
                {
                    isPager = (n.Tag as NodeInfos).isPager;
                    if (isPager)
                    {
                        parentNode = n;
                        break;
                    }
                    else
                    {
                        parentCode = (n.Tag as NodeInfos).ParentCode;
                    }
                }
            }

            return (parentNode.Tag as NodeInfos).Code; 
        }

        private ShapeNode GetParentNode(ShapeNode node)
        {
            ShapeNode pNode = new ShapeNode();
            string parentCode = (node.Tag as NodeInfos).ParentCode;
            foreach (ShapeNode n in this.diagram1.Nodes.Where(i => ((NodeInfos)i.Tag).Code == parentCode))
            {
                pNode = n;
                break;
            }
            return pNode;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //循环找到点击树结构节点对应的shapeNode
            ShapeNode currentSNode = new ShapeNode();
            for (int i = 0; i < this.diagram1.Nodes.Count; i++)
            {
                ShapeNode node = (ShapeNode)this.diagram1.Nodes[i];
                NodeInfos nodeInfo = node.Tag as NodeInfos;
                NodeInfos pnodeInfo = node.Parent.Tag as NodeInfos;
                if (nodeInfo.Code == e.Node.Name)
                {
                    currentSNode = node;
                }

            }
            //通过点击的节点，找到所有子节点
            Stack<ShapeNode> stacks = new Stack<ShapeNode>();
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
                ShapeNode node = (ShapeNode)this.diagram1.Nodes[i];
                node.Visible = false;
            }
            for (int i = 0; i < this.diagram1.Links.Count; i++)
            {
                DiagramLink link = this.diagram1.Links[i];
                link.Visible = false;
            }
            //再将选中的节点进行展示
            foreach (ShapeNode node2 in stacks)
            {
                NodeInfos nodeInfo2 = node2.Tag as NodeInfos;
                for (int i = 0; i < this.diagram1.Nodes.Count; i++)
                {
                    ShapeNode node = (ShapeNode)this.diagram1.Nodes[i];
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
        }

        #endregion
    }
}

