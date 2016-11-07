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
                drawBoundaries(cube[0].cube);
            }
            catch(Exception e)
            {
                Console.Write("Upps. Something went wrong\n"+e);
            }


        }
        private void drawBoundaries(float[] cube)
        {
            float start = cube[16];
            float end = start + 50;

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(end, end, end);
                GL.Vertex3(start, end, end);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(end, end, end);
                GL.Vertex3(end, start, end);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(end, end, end);
                GL.Vertex3(end, end, start);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(start,start,start);
                GL.Vertex3(start, start, end);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(start, start, start);
                GL.Vertex3(start, end, start);
            GL.End();
            
            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(start, start, start);
                GL.Vertex3(end, start, start);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(end, start, end);
                GL.Vertex3(start, start, end);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(end, start, end);
                GL.Vertex3(end, start, start);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(end, start, start);
                GL.Vertex3(end, end, start);
            GL.End();
            
            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(end, end, start);
                GL.Vertex3(start, end, start);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(start, end, start);
                GL.Vertex3(start, end, end);
            GL.End();

            GL.Begin(BeginMode.Lines);
                GL.Color3(System.Drawing.Color.White);
                GL.Vertex3(start, start, end);
                GL.Vertex3(start, end, end);
            GL.End();
        }

    }
}
