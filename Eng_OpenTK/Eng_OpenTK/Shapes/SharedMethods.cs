using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eng_OpenTK.Shapes
{
    class SharedMethods
    {
        public void shapeBoudaries(ref float x, ref float y, ref float z, int partialCount)
        {
            if (x < 0)
                x = (partialCount - Math.Abs(x % partialCount)) - 1;
            else if (x >= partialCount)
                x = (Math.Abs(x % partialCount));

            if (y < 0)
                y = (partialCount - Math.Abs(y % partialCount)) - 1;
            else if (y >= partialCount)
                y = (Math.Abs(y % partialCount));

            if (z < 0)
                z = (partialCount - Math.Abs(z % partialCount)) - 1;
            else if (z >= partialCount)
                z = (Math.Abs(z % partialCount));

        }
    }
}
