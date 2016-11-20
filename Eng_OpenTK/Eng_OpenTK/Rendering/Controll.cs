using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Eng_OpenTK.Rendering
{
    class Controll
    {
        private int count { get; set; }
        private bool full = false;
        public void setVariables(int Count)
        {
            count = Count;
        }
        public int getCount()
        {
            return count;
        }
        public bool isFull()
        {
            return full;
        }
        public void setFull()
        {
            full = true;
        }
        public void setEmpty()
        {
            full = false;
        }
        public void setCount(int Count)
        {
            count = Count;
        }
    }
}
