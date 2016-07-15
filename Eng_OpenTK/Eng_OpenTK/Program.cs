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
    class Program
    {
        private static int width = 800, height = 600;
        private static ShaderProgram program;
        private static VBO<Vector3> cube;
        private static VBO<Vector3> cubeColor;
        private static VBO<int> cubeElements;
        private static int count = 3375;
        private static float angle = 0;
        private static float time = 0, baseTime = 0, frame = 0;
        private static Stopwatch watch;

        public class forcube
        {
            public static VBO<Vector3>[] cube;
            public static int[] x = new int[10];
            public static VBO<Vector3>[] cubeColor;
            public forcube(int count)
            {
                cube = new VBO<Vector3>[count];
                cubeColor = new VBO<Vector3>[count];
            }

            public void calculateMatrix(int length, int count)
            {
                double cR = 0, cB = 0, cG = 0;
                Random rand = new Random();
                double partialCount = Math.Pow(count, (1.0f / 3.0f));

                Console.WriteLine("partialCount = {0}\n (int)partialCount = {1}", partialCount, (int)partialCount);
                for (int x = 0; x < (int)partialCount; x++)
                    for (int y = 0; y < (int)partialCount; y++)
                        for (int z = 0; z < (int)partialCount; z++)
                        {
                            //Console.WriteLine("x = {0}, y = {1}, z = {2}", x, y, z);
                            cR = rand.NextDouble();
                            cG = rand.NextDouble();
                            cB = rand.NextDouble();
                            cube[x * (int)partialCount * (int)partialCount + y * (int)partialCount + z] = new VBO<Vector3>(new Vector3[] {
                            new Vector3(x, y, z), new Vector3(x, y + length, z), new Vector3(x + length, y + length, z), new Vector3(x + length, y, z),
                            new Vector3(x, y, z + length), new Vector3(x, y + length, z + length), new Vector3(x + length, y + length, z + length), new Vector3(x + length, y, z + length),
                            new Vector3(x, y, z), new Vector3(x, y, z + length), new Vector3(x + length, y, z + length), new Vector3(x + length, y, z),
                            new Vector3(x, y + length, z), new Vector3(x, y + length, z + length), new Vector3(x + length, y + length, z + length), new Vector3(x + length, y + length, z),
                            new Vector3(x + length, y, z), new Vector3(x + length, y + length, z), new Vector3(x + length, y + length, z + length), new Vector3(x + length, y, z + length),
                            new Vector3(x, y, z), new Vector3(x, y + length, z), new Vector3(x, y + length, z + length), new Vector3(x, y, z + length)
                        });

                            cubeColor[x * (int)partialCount * (int)partialCount + y * (int)partialCount + z] = new VBO<Vector3>(new Vector3[] {
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB),
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB),
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB),
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB),
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB),
                            new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB), new Vector3(cR, cG, cB)
                        });
                        }
            }
        }

        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow("OpenGL three dimensional grid");

            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutDisplayFunc(OnDisplay);
            Glut.glutCloseFunc(OnClose);

            
            Gl.Enable(EnableCap.DepthTest);


            program = new ShaderProgram(VertexShader, FragmentShader);

            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, 0.1f, 1000f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 100), Vector3.Zero, Vector3.Up));

            forcube cubes = new forcube(count);

            Console.WriteLine(forcube.x[0]);

            try
            {
                cubes.calculateMatrix(1, count);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Something went wrong");
            }



            cube = new VBO<Vector3>(new Vector3[] {
                new Vector3(0,0,1), new Vector3(0,1,1), new Vector3(1,1,1), new Vector3(1,0,1),
                new Vector3(0,0,0), new Vector3(0,1,0), new Vector3(1,1,0), new Vector3(1,0,0),
                new Vector3(0,0,1), new Vector3(0,0,0), new Vector3(1,0,0), new Vector3(1,0,1),
                new Vector3(0,1,1), new Vector3(0,1,0), new Vector3(1,1,0), new Vector3(1,1,1),
                new Vector3(1,0,1), new Vector3(1,1,1), new Vector3(1,1,0), new Vector3(1,0,0),
                new Vector3(0,0,1), new Vector3(0,1,1), new Vector3(0,1,0), new Vector3(0,0,0)
                });


            cubeElements = new VBO<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, BufferTarget.ElementArrayBuffer);

            cubeColor = new VBO<Vector3>(new Vector3[] {
                new Vector3(1,0,0), new Vector3(1,0,0), new Vector3(1,0,0), new Vector3(1,0,0),
                new Vector3(1,0,0), new Vector3(1,0,0), new Vector3(1,0,0), new Vector3(1,0,0),
                new Vector3(0,0,1), new Vector3(0,0,1), new Vector3(0,0,1), new Vector3(0,0,1),
                new Vector3(0,0,1), new Vector3(0,0,1), new Vector3(0,0,1), new Vector3(0,0,1),
                new Vector3(0,1,0), new Vector3(0,1,0), new Vector3(0,1,0), new Vector3(0,1,0),
                new Vector3(0,1,0), new Vector3(0,1,0), new Vector3(0,1,0), new Vector3(0,1,0)
            });

            watch = Stopwatch.StartNew();

            Glut.glutMainLoop();
        }

        private static void OnClose()
        {


            cube.Dispose();
            cubeColor.Dispose();
            cubeElements.Dispose();
            program.DisposeChildren = true;
            program.Dispose();
        }

        private static void OnDisplay()
        {






        }

        private static void OnRenderFrame()
        {

            watch.Stop();
            time += watch.ElapsedMilliseconds;
            watch.Reset();
            watch.Start();
            // Stopwatch.Frequency;

            Gl.Viewport(0, 0, width, height);


            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            program.Use();

            angle += 0.005f;

            //cube
            program["model_matrix"].SetValue(Matrix4.CreateRotationX(-angle) * Matrix4.CreateRotationZ(angle) * Matrix4.CreateRotationY(-angle) * Matrix4.CreateTranslation(new Vector3(-15f, -5f, -100f)));


            try
            {
                double partialCount = Math.Pow(count, (1.0f / 3.0f));
                partialCount = Math.Pow(partialCount, 3);
                for (int i = 0; i < (int)partialCount; i++)
                {
                    //Console.WriteLine(i);
                    Gl.BindBufferToShaderAttribute(forcube.cube[i], program, "vertexPosition");
                    Gl.BindBufferToShaderAttribute(forcube.cubeColor[i], program, "vertexColor");
                    Gl.BindBuffer(cubeElements);

                    Gl.DrawElements(BeginMode.Quads, cubeElements.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Something went wrong");

            }

            frame++;



            if (time > 1000)
            {
                Console.WriteLine("FPS: {0}     time:{1}        freq:{2}    elap:{3}", frame, time, Stopwatch.Frequency, watch.ElapsedMilliseconds);
                string title = "FPS: " + frame;
                Glut.glutSetWindowTitle(title);
                time = 0;
                frame = 0;



            }

           
            //merge requesto

            Glut.glutSwapBuffers();
        }

        public static string VertexShader = @"
                                            in vec3 vertexPosition;
                                            in vec3 vertexColor;
                                            out vec3 color;
                                            uniform mat4 projection_matrix;
                                            uniform mat4 view_matrix;
                                            uniform mat4 model_matrix;
                                            void main(void)
                                            {
                                                color = vertexColor;
                                                gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
                                            }

                                            ";
        public static string FragmentShader = @"
                                            in vec3 color;
                                            out vec4 fragment;
                                            void main(void)
                                            {
                                                fragment = vec4(color,1);
                                            }
                                            ";


    }
}