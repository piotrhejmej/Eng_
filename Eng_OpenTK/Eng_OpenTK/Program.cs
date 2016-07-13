﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using Tao.FreeGlut;



namespace Eng_OpenTK
{
    class Program
    {
        private static int width = 1280, height = 720;
        private static ShaderProgram program;
        private static VBO<Vector3> triangle, square;
        private static VBO<int> triangleElements, squareElements;

        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow("OpenGL three dimensional grid");

            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutDisplayFunc(OnDisplay);

            program = new ShaderProgram(VertexShader, FragmentShader);

            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, 0.1f, 1000f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));

            triangle = new VBO<Vector3>(new Vector3[] { new Vector3(0, 1, 0), new Vector3(-1, -1, 0), new Vector3(1, -1, 0) });
            square = new VBO<Vector3>(new Vector3[] { new Vector3(-1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0) });

            triangleElements = new VBO<int>(new int[] { 0, 1, 2 }, BufferTarget.ElementArrayBuffer);
            squareElements = new VBO<int>(new int[] { 0, 1, 2, 3 }, BufferTarget.ElementArrayBuffer);


            Glut.glutMainLoop();
        }

        private static void OnDisplay()
        {






        }

        private static void OnRenderFrame()
        {
            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            program.Use();

            //triangle

            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(-1.5f, 0, 0)));

            uint vertexPositionIndex = (uint)Gl.GetAttribLocation(program.ProgramID, "vertexPosition");

            Gl.EnableVertexAttribArray(vertexPositionIndex);
            Gl.BindBuffer(triangle);
            Gl.VertexAttribPointer(vertexPositionIndex, triangle.Size, triangle.PointerType, true, 12, IntPtr.Zero);
            Gl.BindBuffer(triangleElements);

            Gl.DrawElements(BeginMode.Triangles, triangleElements.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);




            //square
            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(1.5f, 0, 0)));

            Gl.BindBufferToShaderAttribute(square, program, "vertexPosition");
            Gl.BindBuffer(squareElements);

            Gl.DrawElements(BeginMode.Quads, squareElements.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            


            Glut.glutSwapBuffers(); 
        }

        public static string VertexShader = @"
                                            in vec3 vertexPosition;

                                            uniform mat4 projection_matrix;
                                            uniform mat4 view_matrix;
                                            uniform mat4 model_matrix;

                                            void main(void)
                                            {
                                                gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
                                            }
                                            ";
        public static string FragmentShader = @"
                                            void main(void)
                                            {
                                                gl_FragColor = vec4(1,1,1,1);
                                            }
                                            ";


    }
}
