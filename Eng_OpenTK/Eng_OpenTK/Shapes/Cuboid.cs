using Eng_OpenTK.Rendering;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eng_OpenTK.Shapes
{

    class Cuboid : IShape
    {
        public int x, y, z;
        public int startX, startY, startZ;
        public float[] color;

        public void moveShape(ref Cube.Cell cube)
        {

        }

        public List<Vector4> returnShapeCoords(ref List<Cube.Cell> cube, ref ValuesContainer controls)
        {

            ValuesContainer control = controls;
            SharedMethods shared = new SharedMethods();
            List<Vector4> coordList = new List<Vector4>();
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
                        int cubeCoord = (int)(xx * partialCount * partialCount + yy * partialCount + zz);
                        coordList.Add(new Vector4(xx, yy, zz, cube[cubeCoord].state));
                    }

            return coordList;
        }

        public float[] getColor()
        {
            return color;
        }

        public void moveX(bool direction)
        {
            if(direction)
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
        public void setPos(int x, int y, int z)
        {
            startX = x;
            startY = y;
            startZ = z;
        }
    }
}
