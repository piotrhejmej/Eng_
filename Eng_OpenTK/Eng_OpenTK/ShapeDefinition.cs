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
            int size = parent.getPartialCount();
            int x, y, z;
            int.TryParse(textBox1.Text, out x);
            int.TryParse(textBox2.Text, out y);
            int.TryParse(textBox3.Text, out z);
            DialogResult dialogResult;
            Console.WriteLine(size);

            if (x <= size && y <= size && z <= size)
                passValuesAndKillSelf(x, y, z, ref parent);
            else
                dialogResult = MessageBox.Show("Shape size must fit within grid size. \nYou have exceded those boundaries!");

        }
        void passValuesAndKillSelf(int x, int y, int z, ref MainForm parent)
        {
            parent.defShape(x, y, z);
            this.Close();
            this.Dispose();
        }

    }
}
