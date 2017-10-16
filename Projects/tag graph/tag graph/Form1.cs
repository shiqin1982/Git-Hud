using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tag_graph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            PointF[] cur1 = new PointF[1500];
            for (int i = 0; i < cur1.Length; i++)
            {
                double x = (double)i/100;
                double y = Math.Tan(x) + 1.0 / Math.Tan(60);
                cur1[i] = new PointF((float)i, (float)(y* 10 + 50 ));
            }
            g.DrawLines(Pens.Blue, cur1);
        }
    }
}
