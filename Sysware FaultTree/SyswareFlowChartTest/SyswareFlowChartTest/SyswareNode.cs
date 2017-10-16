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
    public class SyswareNode : ShapeNode
    {
        protected override void SaveTo(BinaryWriter writer, PersistContext context)
        {
            base.SaveTo(writer, context);
        }
        protected override void LoadFrom(BinaryReader reader, PersistContext context)
        {
            base.LoadFrom(reader, context);
        }
        public override void Draw(IGraphics graphics, RenderOptions options)
        {
            base.Draw(graphics, options);
            System.Drawing.SolidBrush mb = new System.Drawing.SolidBrush(Color.White);
            RectangleF b = this.GetBounds();
            RectangleF rectf = new RectangleF();
            rectf.X = b.X;
            rectf.Y = b.Y +0.5f;
            rectf.Width = b.Width;
            rectf.Height = 9;
            StringFormat format1 = new StringFormat();
            format1.Alignment = StringAlignment.Center;
            NodeInfos ni = this.Tag as NodeInfos;
            graphics.DrawString(ni.Jdms, new Font("宋体", 8), mb, rectf, format1);
            //graphics.DrawString(this.Text, new Font("宋体", 11), mysbrush1, rectf, format1);
            if (ni.ItemType == "底")
            {
                System.Drawing.SolidBrush mb1 = new System.Drawing.SolidBrush(Color.Black);
                RectangleF gzl = new RectangleF();
                gzl.X = b.X;
                gzl.Y = b.Y + 24.5f;
                gzl.Width = b.Width;
                gzl.Height = 4;
                string str = "";
                if (ni.Gzl == null)
                    str = "r=0";
                else
                {
                    if (ni.Gzl.Trim() == "")
                        str = "r=0";
                    else
                        str = "r=" + ni.Gzl + "E-6";
                }
                graphics.DrawString(str, new Font("宋体", 9), mb1, gzl, format1);
            }

            this.TextFormat = format1;
            this.TextPadding = new Thickness(0, 16f, 0, 0);

            // graphics.Dispose();
        }
        public SyswareNode Copy()
        {
            SyswareNode ret = new SyswareNode();
            RectangleF nodeBounds = new RectangleF(0, 0, 25, 25);
            ret.SetBounds(nodeBounds, false, false);
            NodeInfos ni = this.Tag as NodeInfos;
            ret.Tag = ni.Clone();
            ret.Font = this.Font;
            ret.Text = this.Text;
            ret.TextFormat = this.TextFormat;
            ret.TextPadding = this.TextPadding;
            return ret;
        }
        public static SyswareNode Initial(ShapeNode sn)
        {
            SyswareNode ret = new SyswareNode();
            RectangleF nodeBounds = sn.Bounds;
            ret.SetBounds(nodeBounds, false, false);
            NodeInfos ni = sn.Tag as NodeInfos;
            ret.Tag = ni.Clone();
            ret.Font = sn.Font;
            ret.Text = sn.Text;
            ret.TextFormat = sn.TextFormat;
            ret.TextPadding = sn.TextPadding;
            return ret;
        }
    }
}
