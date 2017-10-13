using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MindFusion.Diagramming;
using MindFusion.Diagramming.Layout;

namespace FaultTreeCreate
{
    public partial class ParamForm : Form
    {
        private ShapeNode CurrentItem;
        public ParamForm(DiagramItem current)
        {
            InitializeComponent();
            CurrentItem = (ShapeNode)current;
            this.textBoxName.Text = CurrentItem.Text;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            CurrentItem.Text = this.textBoxName.Text;
            this.Close();
        }
    }
}
