﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace winForm_Drawing
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
                double x = (double)i / 10;
                double y = Math.Sin(x)  + Math.Cos(3 );
                cur1[i] = new PointF((float)i, (float)(y *30 +150));
            }
            g.DrawLines(Pens.Blue, cur1);

            //PointF[] cur2 = new PointF[100];
            //for (int i = 0; i < cur2.Length; i++)
            //{
            //    double theta = Math.PI / 50 * i;
            //    double r = Math.Cos(theta * 16);
            //    cur2[i] = new PointF(
            //    (float)(r * Math.Cos(theta) * 50 + 230),
            //    (float)(r * Math.Sin(theta) * 50 + 100));
            //}
            //g.DrawLines(Pens.Blue, cur2);
        }


    }
}