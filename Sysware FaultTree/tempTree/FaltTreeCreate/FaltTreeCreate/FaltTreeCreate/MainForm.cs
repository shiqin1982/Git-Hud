using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MindFusion.Diagramming;
using MindFusion.Diagramming.Layout;

namespace FaultTreeCreate
{
    public partial class MainForm : Form
    {
        RectangleF nodeBounds = new RectangleF(0, 0, 20, 20);
        public MainForm()
        {
            InitializeComponent();
            diagramView.MouseDoubleClick += new MouseEventHandler(diagramView_MouseDoubleClick);
        }

        
        void diagramView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (diagram.ActiveItem == null)
                return;
            ParamForm tmp = new ParamForm(diagram.ActiveItem);
            tmp.ShowDialog();
            //throw new NotImplementedException();

            //if (diagram.ActiveItem != null)
            //    this.toolStripStatusLabel1.Text = "OK";
            //else
            //    this.toolStripStatusLabel1.Text = "NULL";
        }

        private void buttonRoot_Click(object sender, EventArgs e)
        {
            diagram.ClearAll();
            ShapeNode root = diagram.Factory.CreateShapeNode(nodeBounds);
            root.Shape = new Shape(new ElementTemplate[]
	            {
		            new LineTemplate(0, 100, 50, 0),
		            new LineTemplate(50, 0, 100, 100),
		            new LineTemplate(100, 100, 0, 100)
	            },
                FillMode.Winding);
            root.Text = "TOP";
            UserLayOut();
        }

        private void buttonSub_Click(object sender, EventArgs e)
        {
            if (diagram.ActiveItem == null)
                return;
            ShapeNode current = (ShapeNode)diagram.ActiveItem;
            ShapeNode node = diagram.Factory.CreateShapeNode(nodeBounds);
            node.Text = ("Name");
            diagram.Factory.CreateDiagramLink(current, node);
            UserLayOut();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
        private void UserLayOut()
        {
            TreeLayout layout = new TreeLayout();
            layout.Type = TreeLayoutType.Centered;
            layout.Direction = TreeLayoutDirections.TopToBottom;
            layout.LinkStyle = TreeLayoutLinkType.Cascading3;
            layout.NodeDistance = 3;
            layout.LevelDistance = 20; // let horizontal positions overlap
            layout.Arrange(diagram);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (diagram.ActiveItem == null)
                return;

            Stack<DiagramItem> stacks = new Stack<DiagramItem>();
            stacks.Push(diagram.ActiveItem);
            GetSubItems(diagram.ActiveItem, stacks);

            while (stacks.Count > 0)
            {
                diagram.Items.Remove(stacks.Pop());

            }

        }
        private void GetSubItems(DiagramItem current, Stack<DiagramItem> stacks)
        {
            foreach (var link in diagram.Links.Where(w => w.Origin == current))
            {
                DiagramItem next = (DiagramItem)link.Destination;
                stacks.Push(next);
                GetSubItems(next, stacks);
            }
        }
    }
}
