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
            rectf.Y = b.Y + 0.5f;
            rectf.Width = b.Width;
            rectf.Height = 9;
            StringFormat format1 = new StringFormat();
            format1.Alignment = StringAlignment.Center;
            NodeInfos ni = this.Tag as NodeInfos;
            graphics.DrawString(ni.Jdms, new Font("宋体", 8), mb, rectf, format1);
            //graphics.DrawString(this.Text, new Font("宋体", 11), mysbrush1, rectf, format1);
            ////概率分配显示
            ShowGlfp(graphics ,ni);

            if (ni.ItemType == "底")
            {
                string str = GetEventString(ni);
                System.Drawing.SolidBrush mb1 = new System.Drawing.SolidBrush(Color.Black);
                RectangleF gzl = new RectangleF();
                gzl.X = b.X;
                gzl.Y = b.Y + 24.5f;
                gzl.Width = b.Width;
                gzl.Height = 4;
                graphics.DrawString(str, new Font("宋体", 9), mb1, gzl, format1);
            }
            else if (ni.ItemType == "顶")
            {
                string str = GetTopString(ni);
                if (str != "")
                {
                    System.Drawing.SolidBrush mb1 = new System.Drawing.SolidBrush(Color.Black);
                    RectangleF gzl = new RectangleF();
                    gzl.X = b.X;
                    gzl.Y = b.Y + 23.5f;
                    gzl.Width = b.Width;
                    gzl.Height = 4;
                    graphics.DrawString(str, new Font("宋体", 8), mb1, gzl, format1);
                }
            }

            this.TextFormat = format1;
            this.TextPadding = new Thickness(0, 16f, 0, 0);

            // graphics.Dispose();
        }
        private void ShowGlfp(IGraphics graphics, NodeInfos ni)
        {
            string str = GetFpglString(ni);
            if (str != "")
            {
                RectangleF b = this.GetBounds();
                System.Drawing.SolidBrush mb1 = new System.Drawing.SolidBrush(Color.Black);
                RectangleF gzl = new RectangleF();
                gzl.X = b.X;
                gzl.Y = b.Y-3f;
                gzl.Width = b.Width;
                gzl.Height = 3;
                StringFormat format1 = new StringFormat();
                format1.Alignment = StringAlignment.Near;
                graphics.DrawString(str, new Font("宋体", 7), mb1, gzl, format1);
            }
        }
        private string GetFpglString(NodeInfos ni)
        {
            string ret = "";
            if (ni.Fpgl != null)
            {
                if (ni.Fpgl.Trim() != "")
                {
                    double d = double.Parse(ni.Fpgl.Trim()) * 1.0e-6;
                    ret = "P=" + d.ToString("#.##E+0");
                }
            }
            return ret;
        }
        private string GetEventString(NodeInfos ni)
        {
            string ret = "r=0";
            if (ni.AffaType == AffairType.房型事件)
            {
                if (ni.Mxlj.ToLower() == "true")
                    ret = "r=true";
                else
                    ret = "r=false";
            }
            else
            {
                if (ni.Gzl == null)
                    ret = "r=0";
                else
                {
                    if (ni.Gzl.Trim() == "")
                        ret = "r=0";
                    else
                    {
                        double d = double.Parse(ni.Gzl.Trim()) * 1.0e-6;
                        ret = "r=" + d.ToString("#.##E+0");
                    }
                }
            }


            return ret;
        }
        private string GetTopString(NodeInfos ni)
        {
            string ret = "";
            if (ni.Pjsxgl != null)
            {
                if (ni.Pjsxgl.Trim() != "")
                {
                    double d = double.Parse(ni.Pjsxgl.Trim());
                    ret = "Pf=" + d.ToString("#.##E+0");
                }
            }
            return ret;
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
