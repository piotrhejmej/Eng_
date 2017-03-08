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
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = false;
                    textBox5.Visible = false;
                    comboBox3.Visible = false;
                    label3.Text = "Y";
                    label1.Visible = true;
                    label4.Visible = true;
                    label5.Visible = false;
                    label6.Visible = false;
                    break;

                case 1:
                    Console.WriteLine("Inside ShapeChooser, chosen option 1");
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    comboBox3.Visible = true;
                    label3.Text = "Length";
                    label1.Visible = false;
                    label4.Visible = false;
                    label5.Visible = true;
                    label6.Visible = true;
                    break;

                case 2:
                    Console.WriteLine("Inside ShapeChooser, chosen option 2");
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    textBox4.Visible = true;
                    textBox5.Visible = false;
                    comboBox3.Visible = false;
                    label3.Text = "Length";
                    label1.Visible = false;
                    label4.Visible = false;
                    label5.Visible = true;
                    label6.Visible = false;
                    break;

                case 3:
                    Console.WriteLine("Inside ShapeChooser, chosen option 3");
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    comboBox3.Visible = true;
                    label3.Text = "Length";
                    label1.Visible = false;
                    label4.Visible = false;
                    label5.Visible = true;
                    label6.Visible = true;
                    break;

                    

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainWindow parent = (MainWindow)this.Owner;
            int size = parent.getPartialCount();
            int x = 0, y = 0, z = 0, r = 0, a = 0, h = 0;
            int length = 0;

            int.TryParse(textBox4.Text, out r);

            int.TryParse(textBox1.Text, out x);
            int.TryParse(textBox2.Text, out y);
            int.TryParse(textBox3.Text, out z);

            int.TryParse(textBox6.Text, out a);
            int.TryParse(textBox7.Text, out h);

            if (comboBox1.SelectedIndex == 1)
            {
                int.TryParse(textBox5.Text, out length);

                if (comboBox3.SelectedIndex == 0)
                {
                    x = 2 * r;
                    y = 2 * r;
                    z = length;
                }
                if (comboBox3.SelectedIndex == 1)
                {
                    z = 2 * r;
                    x = 2 * r;
                    y = length;
                }
                if (comboBox3.SelectedIndex == 2)
                {
                    z = 2 * r;
                    y = 2 * r;
                    x = length;
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                int.TryParse(textBox5.Text, out length);
                x = 2 * r;
                y = 2 * r;
                z = 2 * r;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                int.TryParse(textBox5.Text, out length);

                if (comboBox3.SelectedIndex == 0)
                {
                    x = a;
                    y = h;
                    z = length;
                }
                if (comboBox3.SelectedIndex == 1)
                {
                    z = a;
                    x = h;
                    y = length;
                }
                if (comboBox3.SelectedIndex == 2)
                {
                    z = a;
                    y = h;
                    x = length;
                }
                int.TryParse(textBox5.Text, out length);
            }

            DialogResult dialogResult;
            Console.WriteLine(size);
            Console.WriteLine("x: {0}, y: {1}, z: {2}, r: {3}", x, y, z, r);
            if (x <= size && y <= size && z <= size && a <= size && h <= size)
                passValuesAndKillSelf(x, y, z, r, ref parent, a, h);
            else
                dialogResult = MessageBox.Show("Shape size must fit within grid size. \nYou have exceded those boundaries!");

        }
        void passValuesAndKillSelf(int x, int y, int z, int r, ref MainWindow parent, int a, int h)
        {
            parent.defShape(x, y, z, (int)comboBox1.SelectedIndex, (int)comboBox3.SelectedIndex, r, (int)comboBox2.SelectedIndex, a, h);
            Console.WriteLine("Selected index: "+comboBox1.SelectedIndex);
            this.Close();
            this.Dispose();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ShapeDefinition_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
    }
}
