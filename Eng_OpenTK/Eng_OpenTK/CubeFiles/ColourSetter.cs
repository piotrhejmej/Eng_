using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eng_OpenTK.Cube
{
    class ColourSetter
    {
        public float[] getColour(int state)
        {
            float cR, cB, cG;
            cR = cB = cG = (float)state / 255;
            float[] stateColour = new float[]
            {
                cR, cB, cG,
                cR, cB, cG,
                cR, cB, cG,
                cR, cB, cG,
                cR, cB, cG,
                cR, cB, cG,
                cR, cB, cG,
                cR, cB, cG,
            };

            return stateColour;
        }
    }
}
