using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eng_OpenTK.Cube
{
    class Assembly
    {
        public Cube buildCube(int X, int Y, int Z, int correction, float length, int count)
        {
            Cube cube = new Cube();
            ColourSetter setters = new ColourSetter();

            cube.x = X;
            cube.y = Y;
            cube.z = Z;

            float x = length * (X - correction);
            float y = length * (Y - correction);
            float z = length * (Z - correction);

            double cR = 0, cB = 0, cG = 0;
            Random rand = new Random((int)DateTime.Now.Ticks);
            double partialCount = Math.Pow(count, (1.0f / 3.0f));

            cube.state = 0;
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
            
            cube.cubeColor = setters.getColour(cube.state);

            cube.triangles = new byte[]
            {
                1, 0, 2, // front
                3, 2, 0,
                6, 4, 5, // back
                4, 6, 7,
                4, 7, 0, // left
                7, 3, 0,
                1, 2, 5, // right
                2, 6, 5,
                0, 1, 5, // top
                0, 5, 4,
                2, 3, 6, // bottom
                3, 7, 6,
            };
            
            return cube;
        }


    }
}
