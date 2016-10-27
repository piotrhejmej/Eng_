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
    public partial class Loader : Form
    {
        public Loader()
        {
            InitializeComponent();
        }
        public void setSize (int size)
        {
            label1.Text = "Cube is being calculated. Please Wait...";
            this.Refresh();
            progressBar1.Maximum = size;

        }
        public void progres(int x, int y, int z)
        {
            //progressBar1.Value = x * x * x + y * y + z;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
