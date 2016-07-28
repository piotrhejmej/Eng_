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
    class CubeRender
    {
        public void Render(List<Cube> cubesy, ShaderProgram program, VBO<int> cubeElements, int count)
        {
#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable CS0168 // Variable is declared but never used

            try
            {
                double partialCount = Math.Pow(count, (1.0f / 3.0f));
                partialCount = Math.Pow(partialCount, 3);

                for (int i = 0; i < (int)partialCount; i++)
                {
                    Gl.BindBufferToShaderAttribute(cubesy[i].cube, program, "vertexPosition");
                    Gl.BindBufferToShaderAttribute(cubesy[i].cubeColor, program, "vertexColor");
                    Gl.BindBuffer(cubeElements);


                    Gl.DrawElements(BeginMode.Quads, cubeElements.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);


                }
            }

            catch (NullReferenceException ex)
            {
                Console.WriteLine("Something went wrong");
            }


#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0168 // Variable is declared but never used
        }
    }
}
