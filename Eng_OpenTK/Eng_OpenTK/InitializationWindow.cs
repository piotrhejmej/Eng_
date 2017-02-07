using Eng_OpenTK.Rendering;
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
    public partial class InitializationWindow : Form
    {
        bool close = false;
        public InitializationWindow()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainWindow parent = (MainWindow)this.Owner;
            parent.ShowInTaskbar = false;

            int axisConcetration;
            int.TryParse(textBox1.Text, out axisConcetration);
            
                passVariablesAndCloseSelf(axisConcetration, ref parent);
            
        }
        private void passVariablesAndCloseSelf(int axisConcetration, ref MainWindow parent)
        {
            parent.setVars(axisConcetration);
            this.Close();
            parent.Opacity = 100;
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            close = true;
            Application.Exit();
        }

        private void InitializationWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(close == false)
                e.Cancel = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void InitializationWindow_Load(object sender, EventArgs e)
        {
            
        }
    }
}
