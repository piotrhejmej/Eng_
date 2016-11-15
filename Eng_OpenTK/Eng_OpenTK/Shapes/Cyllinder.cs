using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eng_OpenTK.Cube;
using Eng_OpenTK.Rendering;
using OpenTK;

namespace Eng_OpenTK.Shapes
{
    class Cyllinder : IShape
    {
        public int x, y, z, r;
        public int startX, startY, startZ;
        public float[] color, prevColor;
        public int prevState;

        public float[] getColor()
        {
            return color;
        }

        public void moveShape(ref Cube.Cube cube)
        {
            throw new NotImplementedException();
        }

        public void moveX(bool direction)
        {
            if (direction)
                startX++;
            else
                startX--;

        }
        public void moveY(bool direction)
        {
            if (direction)
                startY++;
            else
                startY--;

        }
        public void moveZ(bool direction)
        {
            if (direction)
                startZ++;
            else
                startZ--;
        }

        public List<Vector4> returnShapeCoords(ref List<Cube.Cube> cube, ref Controll controls)
        {
            Controll control = controls;
            SharedMethods shared = new SharedMethods();
            List<Vector4> coordList = new List<Vector4>();
            Vector2 S = new Vector2((startX + r), (startY + r));

            Vector3 coord = new Vector3(startX, startY, startZ);
            int partialCount = (int)(Math.Pow(control.getCount(), 1.0f / 3.0f));
            float xx, yy, zz;

            for (float i = startX; i < x + startX; i++)
                for (float j = startY; j < y + startY; j++)
                    for (float k = startZ; k < z + startZ; k++)
                    {
                        xx = i;
                        yy = j;
                        zz = k;

                        shared.shapeBoudaries(ref xx, ref yy, ref zz, partialCount);
                        if((Math.Pow(xx-S.X, 2) + Math.Pow(yy - S.Y, 2)) < Math.Pow(r,2))
                        {
                            int cubeCoord = (int)(xx * partialCount * partialCount + yy * partialCount + zz);
                            coordList.Add(new Vector4(xx, yy, zz, cube[cubeCoord].state));
                        }
                    }

            return coordList;
        }
    }
}
