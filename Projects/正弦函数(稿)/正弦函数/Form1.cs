using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 正弦函数
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            PointF[] cur1 = new PointF[1500];
            for (int i = 0; i < cur1.Length; i++)
            {
                double x = (double)i / 20;
                double y = Math.Sin(x) + Math.Cos(3);
                cur1[i] = new PointF((float)i, (float)(y * 30 + 150));
            }
            g.DrawLines(Pens.Blue, cur1);
            
        }
    }
}
