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
        private static int width = 1280, height = 720;
        private static ShaderProgram program;
        private static VBO<int> cubeElements;
        private static int count = 85184;
        private static float time = 0, frame = 0;
        private static Stopwatch watch;
                        
        private static List<Cube> cubesy = new List<Cube>();
        private static Assembly assembly = new Assembly();
        private static CubeRender cubeRender = new CubeRender();

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
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(50, 50, 200), Vector3.Zero, Vector3.Up));
            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(-45f, -25f, -0f)));
            
           

            double partialCount = Math.Pow(count, (1.0f / 3.0f));
            for (int x = 0; x < (int)partialCount; x++)
                for (int y = 0; y < (int)partialCount; y++)
                    for (int z = 0; z < (int)partialCount; z++)
                    {
                        assembly.buildCube(x,y,z,1, count, ref cubesy);
                    }
            



            cubeElements = new VBO<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, BufferTarget.ElementArrayBuffer);



            watch = Stopwatch.StartNew();

                      

            Glut.glutMainLoop();
        }

        private static void OnClose()
        {
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

            
        Gl.Viewport(0, 0, width, height);


            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            program.Use();
            
            
            cubeRender.Render(cubesy, program, cubeElements, count);

            frame++;



            if (time > 1000)
            {
                Console.Clear();
                Console.WriteLine("FPS: {0}     time:{1}        freq:{2}    elap:{3}", frame, time, Stopwatch.Frequency, watch.ElapsedMilliseconds);
                string title = "FPS: " + frame;
                Glut.glutSetWindowTitle(title);
                time = 0;
                frame = 0;



            }

           
            

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