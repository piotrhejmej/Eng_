using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eng_OpenTK.Cube
{
    class ColourSetter
    {
        public float[] getColour(int r, int g, int b)
        {
            float cR = (float)r / 100;
            float cG = (float)g / 100;
            float cB = (float)b / 100;

            float[] stateColour = new float[]
            {
                cR, cG, cB,
                cR, cG, cB,
                cR, cG, cB,
                cR, cG, cB,
                cR, cG, cB,
                cR, cG, cB,
                cR, cG, cB,
                cR, cG, cB,
            };

            return stateColour;
        }
        public float[] getColour(float[] color)
        {
            float[] stateColour = color;

            return stateColour;
        }
    }
}
