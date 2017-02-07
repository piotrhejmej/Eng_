using Eng_OpenTK.Rendering;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eng_OpenTK.Shapes
{
    interface IShape
    {
        void moveShape(ref Cube.Cell cube);
        List<Vector4> returnShapeCoords(ref List<Cube.Cell> cube, ref ValuesContainer controls);
        float[] getColor();
        void moveX(bool direction);
        void moveY(bool direction);
        void moveZ(bool direction);
        void setPos(int x, int y, int z);
    }
}
