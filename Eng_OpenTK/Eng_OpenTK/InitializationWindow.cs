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
            MainForm parent = (MainForm)this.Owner;
            parent.ShowInTaskbar = false;

            int tempCount;
            int.TryParse(textBox1.Text, out tempCount);

            if (tempCount < 100 || checkBox1.Checked)
            {
                passVariablesAndCloseSelf(tempCount, ref parent);
            }
            else if (tempCount < 100)
            {
                DialogResult dialogResult = MessageBox.Show("Size larger than 100 may cause problems on lower class PCs. \nAre you certain to use "+tempCount+" as the Size Value?", "Are you Sure?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    passVariablesAndCloseSelf(tempCount, ref parent);
                }
            }
            else if(tempCount>=160)
            {
                MessageBox.Show("Chosing size of 3D grid to be higher than 160 will cause Memory Overflow errors. \nI'm aware of this limitation and it will be corrected in future builds", "WARNING");
            }
        }
        private void passVariablesAndCloseSelf(int tempCount, ref MainForm parent)
        {
            parent.setVars(tempCount);
            this.Close();
            parent.Opacity = 100;
            parent.ShowInTaskbar = true;
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
            if (checkBox1.Checked)
                checkBox1.Text = "Hardcore Mode = ON";
            else
                checkBox1.Text = "Hardcore Mode = OFF";
        }

        private void InitializationWindow_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
