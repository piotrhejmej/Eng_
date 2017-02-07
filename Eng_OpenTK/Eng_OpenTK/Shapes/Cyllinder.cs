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
        public float[] color;
        public int orientation;

        public float[] getColor()
        {
            return color;
        }

        public void moveShape(ref Cube.Cell cube)
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

        public List<Vector4> returnShapeCoords(ref List<Cube.Cell> cube, ref ValuesContainer controls)
        {
            ValuesContainer control = controls;
            SharedMethods shared = new SharedMethods();
            List<Vector4> coordList = new List<Vector4>();
            Vector2 S = new Vector2(0, 0);

            if (orientation == 0)
                S = new Vector2((startX + r), (startY + r));
            if (orientation == 1)
                S = new Vector2((startX + r), (startZ + r));
            if (orientation == 2)
                S = new Vector2((startY + r), (startZ + r));

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

                        if(orientation == 0 && ((Math.Pow(xx-S.X, 2) + Math.Pow(yy - S.Y, 2)) < Math.Pow(r,2)))
                        {
                            shared.shapeBoudaries(ref xx, ref yy, ref zz, partialCount);
                            int cubeCoord = (int)(xx * partialCount * partialCount + yy * partialCount + zz);
                            coordList.Add(new Vector4(xx, yy, zz, cube[cubeCoord].state));
                        }
                        if (orientation == 1 && ((Math.Pow(xx - S.X, 2) + Math.Pow(zz - S.Y, 2)) < Math.Pow(r, 2)))
                        {
                            shared.shapeBoudaries(ref xx, ref yy, ref zz, partialCount);
                            int cubeCoord = (int)(xx * partialCount * partialCount + yy * partialCount + zz);
                            coordList.Add(new Vector4(xx, yy, zz, cube[cubeCoord].state));
                        }
                        if (orientation == 2 && ((Math.Pow(yy - S.X, 2) + Math.Pow(zz - S.Y, 2)) < Math.Pow(r, 2)))
                        {
                            shared.shapeBoudaries(ref xx, ref yy, ref zz, partialCount);
                            int cubeCoord = (int)(xx * partialCount * partialCount + yy * partialCount + zz);
                            coordList.Add(new Vector4(xx, yy, zz, cube[cubeCoord].state));
                        }
                    }

            return coordList;
        }

        public void setPos(int x, int y, int z)
        {
            startX = x;
            startY = y;
            startZ = z;
        }
    }
}
