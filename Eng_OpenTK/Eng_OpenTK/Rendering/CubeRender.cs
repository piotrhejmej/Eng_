using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eng_OpenTK.Cube;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Eng_OpenTK.Rendering
{
    class CubeRender
    {
        public void Render(List<Cube.Cube> cube, int count)
        {
            
            try
            {
                double partialCount = Math.Pow(count, (1.0f / 3.0f));
                partialCount = Math.Pow(partialCount, 3);
                unsafe
                {
                    
                    for (int i = 0; i < (int)partialCount; i++)
                    {
                        fixed (float* pcube = cube[i].cube, pcubeColors = cube[i].cubeColor)
                        {
                            fixed (byte* ptriangles = cube[i].triangles)
                            {
                                if (cube[i].state != 0)
                                {
                                    GL.VertexPointer(3, VertexPointerType.Float, 0, new IntPtr(pcube));
                                    GL.EnableClientState(ArrayCap.VertexArray);

                                    GL.ColorPointer(3, ColorPointerType.Float, 0, new IntPtr(pcubeColors));
                                    GL.EnableClientState(ArrayCap.ColorArray);


                                    GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedByte, new IntPtr(ptriangles));
                                }
                            }
                        }
                    }
                }
               // drawBoundaries();
            }
            catch(Exception e)
            {
                Console.Write("Upps. Something went wrong\n");
            }


        }
        private void drawBoundaries()
        {
            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(-25, -25, -25);
                GL.Vertex3(25, -25, -25);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(-25, -25, -25);
                GL.Vertex3(-25, 25, -25);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(-25, -25, -25);
                GL.Vertex3(-25, -25, 25);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(25,25,25);
                GL.Vertex3(25, 25, -25);
            GL.End();

            GL.Begin(BeginMode.Lines);
            GL.Color3(System.Drawing.Color.White);
            GL.Vertex3(25, 25, 25);
            GL.Vertex3(25, -25, 25);
            GL.End();
            
            GL.Begin(BeginMode.Lines);
            GL.Color3(System.Drawing.Color.White);
            GL.Vertex3(25, 25, 25);
            GL.Vertex3(-25, 25, 25);
            GL.End();

            GL.Begin(BeginMode.Lines);
            GL.Color3(System.Drawing.Color.White);
            GL.Vertex3(-25, 25, -25);
            GL.Vertex3(25, 25, -25);
            GL.End();

            GL.Begin(BeginMode.Lines);
            GL.Color3(System.Drawing.Color.White);
            GL.Vertex3(-25, 25, -25);
            GL.Vertex3(-25, 25, 25);
            GL.End();

            GL.Begin(BeginMode.Lines);
            GL.Color3(System.Drawing.Color.White);
            GL.Vertex3(-25, 25, 25);
            GL.Vertex3(-25, -25, 25);
            GL.End();
            
            GL.Begin(BeginMode.Lines);
            GL.Color3(System.Drawing.Color.White);
            GL.Vertex3(-25, -25, 25);
            GL.Vertex3(25, -25, 25);
            GL.End();

            GL.Begin(BeginMode.Lines);
            GL.Color3(System.Drawing.Color.White);
            GL.Vertex3(25, -25, 25);
            GL.Vertex3(25, -25, -25);
            GL.End();

            GL.Begin(BeginMode.Lines);
            GL.Color3(System.Drawing.Color.White);
            GL.Vertex3(25, 25, -25);
            GL.Vertex3(25, -25, -25);
            GL.End();
        }

    }
}
