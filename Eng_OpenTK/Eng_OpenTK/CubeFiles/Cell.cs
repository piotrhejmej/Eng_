using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eng_OpenTK.Cube
{
    public class Cell
    {
        public int x, y, z;
        public float[] cell, cellColor, prevColor;
        public int state, prevState;
    }

    public class shapeTypeAndRate
    {
        public int Type;
        public double fillRate;
    }
}
