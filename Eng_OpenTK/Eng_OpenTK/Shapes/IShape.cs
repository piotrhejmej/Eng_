using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eng_OpenTK.Shapes
{
    interface IShape
    {
        void moveShape(ref Cube.Cube cube);
    }
}
