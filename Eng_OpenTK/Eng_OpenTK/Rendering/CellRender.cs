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
using System.Threading;

namespace Eng_OpenTK.Rendering
{
    class CellRender
    {
        private byte[] _triangles = new byte[]
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

        public void Render(List<Cell> cells, ValuesContainer valContainer)
        {
            int partialCount = (int)Math.Pow(valContainer.getCount(), 1.0f / 3.0f);
            int maxX = valContainer.getX();
            int maxY = valContainer.getY();
            int maxZ = valContainer.getZ();

            bool isFull = valContainer.isFull();
            try
            {
                unsafe
                {
                    for (int x = 0; x < maxX; x++)
                        for (int y = 0; y < maxY; y++)
                            for (int z = 0; z < maxZ; z++)
                            {
                                int cubeCoord = (int)(x * partialCount * partialCount + y * partialCount + z);
                                fixed (float* pcell = cells[cubeCoord].cell, pcellCollor = cells[cubeCoord].cellColor)
                                {
                                    fixed (byte* ptriangles = _triangles)
                                    {
                                        if (cells[cubeCoord].state != 0 && !isFull)
                                        {
                                            GL.VertexPointer(3, VertexPointerType.Float, 0, new IntPtr(pcell));
                                            GL.EnableClientState(ArrayCap.VertexArray);

                                            GL.ColorPointer(3, ColorPointerType.Float, 0, new IntPtr(pcellCollor));
                                            GL.EnableClientState(ArrayCap.ColorArray);

                                            GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedByte, new IntPtr(ptriangles));
                                        }
                                        if(isFull && (x == 0 || x == maxX-1 || y == 0 || y == maxY - 1 || z == 0 || z == maxZ - 1))
                                        {
                                            if(valContainer.getDrawGrainsState() && cells[cubeCoord].state > 0)
                                            {
                                                GL.VertexPointer(3, VertexPointerType.Float, 0, new IntPtr(pcell));
                                                GL.EnableClientState(ArrayCap.VertexArray);

                                                GL.ColorPointer(3, ColorPointerType.Float, 0, new IntPtr(pcellCollor));
                                                GL.EnableClientState(ArrayCap.ColorArray);

                                                GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedByte, new IntPtr(ptriangles));
                                            }
                                            if (valContainer.getDrawShapeState() && cells[cubeCoord].state < 0)
                                            {
                                                GL.VertexPointer(3, VertexPointerType.Float, 0, new IntPtr(pcell));
                                                GL.EnableClientState(ArrayCap.VertexArray);

                                                GL.ColorPointer(3, ColorPointerType.Float, 0, new IntPtr(pcellCollor));
                                                GL.EnableClientState(ArrayCap.ColorArray);

                                                GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedByte, new IntPtr(ptriangles));
                                            }

                                        }
                                    }
                                }

                            }                    
                }
                drawBoundaries(cells[0].cell);
            }
            catch(Exception e)
            {
                Console.Write("Upps. Something went wrong\n" + e);
                Thread.Sleep(100);
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
