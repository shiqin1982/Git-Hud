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
        public void Should_auto_curry_two_number_add_function()
        {
            //Arrange
            var add=_curringReasoning.AddTwoNumber();
            var addCurrying = add.Curry();

            //Act
            var result = addCurrying(1)(2);
            
            //Assert
            result.Should().Be(3);
        }
    }
}
