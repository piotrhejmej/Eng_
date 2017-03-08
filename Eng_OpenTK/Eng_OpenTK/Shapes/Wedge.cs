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
    class Wedge : IShape
    {
        public int x, y, a, h, z, r;
        public int startX, startY, startZ;
        public float[] color;
        public int orientation, hOrientation;
        private int _a, _h;

        public float[] getColor()
        {
            return color;
        }

        public void moveShape(ref Cell cube)
        {
            return;
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

        public List<Vector4> returnShapeCoords(ref List<Cell> cube, ref ValuesContainer controls)
        {
            ValuesContainer control = controls;
            SharedMethods shared = new SharedMethods();
            List<Vector4> coordList = new List<Vector4>();
            Vector2 S = new Vector2(0, 0);
            int partialCount = (int)(Math.Pow(control.getCount(), 1.0f / 3.0f));

            if (orientation == 0)
                S = new Vector2((startX + (a / 2)), (startY + (h / 2)));
            if (orientation == 1)
                S = new Vector2((startX + (a / 2)), (startZ + (h / 2)));
            if (orientation == 2)
                S = new Vector2((startY + (a / 2)), (startZ + (h / 2)));

            _a = a;
            _h = h;

            float xx, yy, zz;

            for (float i = startX; i < x + startX; i++)
                for (float j = startY; j < y + startY; j++)
                    for (float k = startZ; k < z + startZ; k++)
                    {
                        xx = i;
                        yy = j;
                        zz = k;

                        if (orientation == 0 && isInTriangleBoundaries(xx, yy, startX, startY) && isInCircleBoundaries(S, xx, yy, r))
                        {
                            shared.shapeBoudaries(ref xx, ref yy, ref zz, partialCount);
                            int cubeCoord = (int)(xx * partialCount * partialCount + yy * partialCount + zz);
                            coordList.Add(new Vector4(xx, yy, zz, cube[cubeCoord].state));
                        }
                        if (orientation == 1 && isInTriangleBoundaries(xx, zz, startX, startZ) && isInCircleBoundaries(S, xx, zz, r))
                        {
                            shared.shapeBoudaries(ref xx, ref yy, ref zz, partialCount);
                            int cubeCoord = (int)(xx * partialCount * partialCount + yy * partialCount + zz);
                            coordList.Add(new Vector4(xx, yy, zz, cube[cubeCoord].state));
                        }
                        if (orientation == 2 && isInTriangleBoundaries(yy, zz, startY, startZ) && isInCircleBoundaries(S, yy, zz, r))
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

        private bool isInCircleBoundaries(Vector2 S, float x, float y, int r)
        {
            if (((Math.Pow(x - S.X, 2) + Math.Pow(y - S.Y, 2)) < Math.Pow(r, 2)))
                return true;

            return false;
        }
        private bool isInTriangleBoundaries(float x, float y, int startX, int startY)
        {
            if ( ((y-startY)*(_a/2))-(_h*(x - startX)) < 0  &&
                ( (y-startY)*(-(_a/2)) - (_h * (x-_a-startX))  ) > 0 )
                return true;

            return false;
        }

    }
}
