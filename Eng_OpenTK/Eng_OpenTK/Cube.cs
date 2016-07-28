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
    public class Cube
    {
        public VBO<Vector3> cube;
        public VBO<Vector3> cubeColor;
        public int state;
        public int posX, posY, posZ;


    }
}
