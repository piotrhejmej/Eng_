using System;
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

        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow("OpenGL three dimensional grid");

            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutDisplayFunc(OnDisplay);
            
            Glut.glutMainLoop();
        }

        private static void OnDisplay() { }

        private static void OnRenderFrame()
        {
            


            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Glut.glutSwapBuffers(); 
        }
    }
}
