using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using Tao.FreeGlut;
using System.Diagnostics;

namespace Eng_OpenTK
{
    public class Assembly
    {
        public void buildCube(int x, int y, int z, int length, int count, ref List<Cube> tempList)
        {
            Cube cube = new Cube();

            double cR = 0, cB = 0, cG = 0;
            Random rand = new Random();
            double partialCount = Math.Pow(count, (1.0f / 3.0f));


            cR = rand.NextDouble();
            cG = rand.NextDouble() * x * 0.1f;
            cB = rand.NextDouble();
            cube.cube = new VBO<Vector3>(new Vector3[] {
                            new Vector3(x, y, z), new Vector3(x, y + length, z), new Vector3(x + length, y + length, z), new Vector3(x + length, y, z),
                            new Vector3(x, y, z + length), new Vector3(x, y + length, z + length), new Vector3(x + length, y + length, z + length), new Vector3(x + length, y, z + length),
                            new Vector3(x, y, z), new Vector3(x, y, z + length), new Vector3(x + length, y, z + length), new Vector3(x + length, y, z),
                            new Vector3(x, y + length, z), new Vector3(x, y + length, z + length), new Vector3(x + length, y + length, z + length), new Vector3(x + length, y + length, z),
                            new Vector3(x + length, y, z), new Vector3(x + length, y + length, z), new Vector3(x + length, y + length, z + length), new Vector3(x + length, y, z + length),
                            new Vector3(x, y, z), new Vector3(x, y + length, z), new Vector3(x, y + length, z + length), new Vector3(x, y, z + length)
                        });

            cube.cubeColor = new VBO<Vector3>(new Vector3[] {
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB),
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB),
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB),
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB),
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB),
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB)
                        });


            tempList.Add(cube);
        }
    }
}
