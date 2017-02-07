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
    class Sphere : IShape
    {
        public int r;
        public int startX, startY, startZ;
        public float[] color;

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
            
            Vector3 S = new Vector3((startX + r), (startY + r), (startZ + r));
            
            Vector3 coord = new Vector3(startX, startY, startZ);
            int partialCount = (int)(Math.Pow(control.getCount(), 1.0f / 3.0f));
            float xx, yy, zz;

            for (float i = startX; i < r * 2 + startX; i++)
                for (float j = startY; j < r * 2 + startY; j++)
                    for (float k = startZ; k < r * 2 + startZ; k++)
                    {
                        xx = i;
                        yy = j;
                        zz = k;

                        if (((Math.Pow(xx - S.X, 2) + Math.Pow(yy - S.Y, 2) + Math.Pow(zz - S.Z, 2)) < Math.Pow(r, 2)))
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
