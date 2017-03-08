using Eng_OpenTK.Cube;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eng_OpenTK
{    
    public partial class RandomShapeGenerator : Form
    {
        private MainWindow _parent;
        public RandomShapeGenerator()
        {
            InitializeComponent();
        }

        private void RandomShapeGenerator_Load(object sender, EventArgs e)
        {
            _parent = (MainWindow)this.Owner;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double fillRate;
            int baseAxis = 0;
            double.TryParse(textBox1.Text, out fillRate);
            List<shapeTypeAndRate> shapes = new List<shapeTypeAndRate>();
            shapeTypeAndRate temp1, temp2, temp3, temp4 = new shapeTypeAndRate();
            if (checkBox1.Checked)
            {
                shapes.Add(new shapeTypeAndRate { Type = 0 });
            }                
            if (checkBox2.Checked)
            {
                shapes.Add(new shapeTypeAndRate { Type = 1 });
            }
            if (checkBox3.Checked)
            {
                shapes.Add(new shapeTypeAndRate { Type = 2 });
            }
            if (checkBox4.Checked)
            {
                shapes.Add(new shapeTypeAndRate { Type = 3 });
            }

            if (radioButton1.Checked)
                baseAxis = 0;
            if (radioButton2.Checked)
                baseAxis = 1;
            if (radioButton3.Checked)
                baseAxis = 2;
            if (radioButton4.Checked)
                baseAxis = -1;

            _parent.randomShapeGenerate(fillRate, baseAxis, shapes);
            this.Close();
            this.Dispose();
        }
    }
}
