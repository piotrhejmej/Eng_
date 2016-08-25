using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Eng_OpenTK.Rendering
{
    class Setup
    {
        public void SetPerspectiveProjection(int width, int height, float FOV, Matrix4 projectionMatrix)
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * (FOV / 180f), width / (float)height, 0.2f, 1000f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix);
        }

        public void SetLookAtCamera(Matrix4 modelViewMatrix)
        {
            modelViewMatrix = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);
        }

        public void SetupViewport(Matrix4 modelViewMatrix, Matrix4 projectionMatrix, int width, int height)
        {
            GL.MatrixMode(MatrixMode.Projection);
            SetPerspectiveProjection(width, height, 45, projectionMatrix);
            Vector3 cameraPosition = new Vector3(0, 0, -40);
            Vector3 cameraTarget = new Vector3(100, 20, 0);
            SetLookAtCamera(modelViewMatrix);
        }
        
        public void OrthoView(Matrix4 projectionMatrix, int width, int height)
        {
            projectionMatrix = Matrix4.Identity;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity(); // reset matrix
            GL.Ortho(0, width, 0, height, -10000, 10000);
        }


    }
}
