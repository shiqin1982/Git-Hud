using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //坐标轴
            e.Graphics.DrawLine(Pens.Black, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);
            e.Graphics.DrawLine(Pens.Black, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);
             //坐标刻度
            for (int i = 1; i < 10; i++)
            {
                e.Graphics.DrawLine(Pens.Black, pictureBox1.Width / 10 * i, pictureBox1.Height / 2, pictureBox1.Width / 10 * i, pictureBox1.Height / 2 - 5);
                e.Graphics.DrawString((i - 5).ToString(), new Font("宋体", 9), new SolidBrush(Color.Black), pictureBox1.Width / 10 * i - 10, pictureBox1.Height / 2);
            }
            for (int j = 1; j < 4; j++)
            {
                e.Graphics.DrawLine(Pens.Black, pictureBox1.Width / 2, pictureBox1.Height / 4 * j, pictureBox1.Width / 2 + 5, pictureBox1.Height / 4 * j);
                if (j - 2 != 0)
                    e.Graphics.DrawString((j - 2).ToString(), new Font("宋体", 9), new SolidBrush(Color.Black), pictureBox1.Width / 2 - 15, pictureBox1.Height / 4 * j);

            }
            int size = 170;//波形周期
            double[] x = new double[size];
            Pen pen = new Pen(Color.Red);
            int val = 2;
            float temp = 0.0f;
            e.Graphics.TranslateTransform(pictureBox1.Width / 4 - 30, pictureBox1.Height / 2);//x,y平移
            for (int i = 0; i < size; i++)
            {
                x[i] = Math.Sin(2 * Math.PI * i / pictureBox1.Width * 10 / 2) * pictureBox1.Height / 4;
                e.Graphics.DrawLine(pen, i * val, temp, i * val + val / 2, (float)x[i]);
                temp = (float)x[i];
            }
        }
    }
    class Value
    {
        public int val { get; set; }
        public int size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        
    }  
}
