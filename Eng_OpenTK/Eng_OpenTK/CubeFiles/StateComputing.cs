using Eng_OpenTK.Cube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eng_OpenTK.CubeFiles
{
    static class StateComputing
    {
        public static void fillSolidState(ref List<Cube.Cube> cells, int stateIterator)
        {
            Random rand = new Random();
            int cB = rand.Next(100);
            int cG = rand.Next(100);
            int cR = rand.Next(100);

            foreach (Cube.Cube item in cells)
            {
                if (item.state == 0)
                {
                    item.state = stateIterator;
                    item.cellColor = ColourSetter.getColour(cR, cG, cB);
                }

            }
        }
    }

}
