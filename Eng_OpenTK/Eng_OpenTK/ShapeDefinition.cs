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
    public partial class ShapeDefinition : Form
    {
        public ShapeDefinition()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Console.WriteLine("Inside ShapeChooser, chosen option 0");
                    break;

                case 1:
                    Console.WriteLine("Inside ShapeChooser, chosen option 1");
                    break;

                case 2:
                    Console.WriteLine("Inside ShapeChooser, chosen option 2");
                    break;

                case 3:
                    Console.WriteLine("Inside ShapeChooser, chosen option 3");
                    break;

                    

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm parent = (MainForm)this.Owner;
            int x, y, z;
            int.TryParse(textBox1.Text, out x);
            int.TryParse(textBox2.Text, out y);
            int.TryParse(textBox3.Text, out z);
            parent.defShape(z,y,z);
            this.Close();
            this.Dispose();
        }
    }
}
