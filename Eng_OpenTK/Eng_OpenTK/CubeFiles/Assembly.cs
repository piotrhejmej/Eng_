﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eng_OpenTK.Cube
{
    class Assembly
    {
        public void buildCube(int x, int y, int z, int length, int count, ref List<Cube> tempList)
        {
            Cube cube = new Cube();

            double cR = 0, cB = 0, cG = 0;
            Random rand = new Random((int)DateTime.Now.Ticks);
            double partialCount = Math.Pow(count, (1.0f / 3.0f));

            cR = rand.NextDouble();
            cG = rand.NextDouble();
            cB = rand.NextDouble();

            cube.cube = new float[]
            {
                x,              y,              z,
                x,              y - length,     z,
                x - length,     y - length,     z,
                x - length,     y,              z,
                x,              y,              z - length,
                x,              y - length,     z - length,
                x - length,     y - length,     z - length,
                x - length,     y,              z - length,
            };

            cube.cubeColor = new float[]
            {
                (float)cR, (float)cB, (float)cG,
                (float)cR, (float)cB, (float)cG,
                (float)cR, (float)cB, (float)cG,
                (float)cR, (float)cB, (float)cG,
                (float)cR, (float)cB, (float)cG,
                (float)cR, (float)cB, (float)cG,
                (float)cR, (float)cB, (float)cG,
                (float)cR, (float)cB, (float)cG,
            };

            cube.triangles = new byte[]
            {
                1, 0, 2, // front
                3, 2, 0,
                6, 4, 5, // back
                4, 6, 7,
                4, 7, 0, // left
                7, 3, 0,
                1, 2, 5, //right
                2, 6, 5,
                0, 1, 5, // top
                0, 5, 4,
                2, 3, 6, // bottom
                3, 7, 6,
            };

            tempList.Add(cube);
            //Thread.Sleep(1);
        }


    }
}