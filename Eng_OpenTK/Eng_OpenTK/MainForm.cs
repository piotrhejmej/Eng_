using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Configuration;
using System.Threading;

namespace Eng_OpenTK
{
    public partial class MainForm : Form
    {
        bool loaded = false;
        float x = 0, y = 0, z = -50;

        int templicz = 0;

        #region Cube information

        float[] cubeColors = {
            1.0f, 0.0f, 0.0f, 1.0f,
            0.0f, 1.0f, 0.0f, 1.0f,
            0.0f, 0.0f, 1.0f, 1.0f,
            0.0f, 1.0f, 1.0f, 1.0f,
            1.0f, 0.0f, 0.0f, 1.0f,
            0.0f, 1.0f, 0.0f, 1.0f,
            0.0f, 0.0f, 1.0f, 1.0f,
            0.0f, 1.0f, 1.0f, 1.0f,
        };

        byte[] triangles =
        {
            1, 0, 2, // front
			3, 2, 0,
            6, 4, 5, // back
			4, 6, 7,
            4, 7, 0, // left
			7, 3, 0,
            1, 2, 5, //right
			2, 6, 5,
            0, 1, 5, // top
			0, 5, 4,
            2, 3, 6, // bottom
			3, 7, 6,
        };

        float[] cube = {
           -1f, 1f,  -1f, // vertex[0]
		   1f,  1f,  -1f, // vertex[1]
		   1f, -1f,  -1f, // vertex[2]
		   -1f,-1f,  -1f, // vertex[3]
		   -1f, 1f,  -1f, // vertex[4]
		   1f,  1f,  -1f, // vertex[5]
		   1f, -1f,  -1f, // vertex[6]
		   -1f,-1f,  -1f, // vertex[7]
		};

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {

            loaded = true;
            GL.ClearColor(0.145f, 0.145f, 0.145f, 0);
            SetupViewport();
            resize();

        }
        
        private Matrix4 projectionMatrix;
        private Matrix4 modelViewMatrix, temp;
        private Vector3 cameraPosition;
        private Vector3 cameraTarget;
        private Vector3 cameraUp = Vector3.UnitY;


        private void SetPerspectiveProjection(int width, int height, float FOV)
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * (FOV / 180f), width / (float)height, 0.2f, 1000f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix); // this replaces the old matrix, no need for GL.LoadIdentity()
        }

        private void SetLookAtCamera(Vector3 position, Vector3 target, Vector3 up)
        {
            modelViewMatrix = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);

        }

        private void SetupViewport()
        {
            int w = glControl1.Width;
            int h = glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);
            SetPerspectiveProjection(w, h, 45);
            cameraPosition = new Vector3(0, 0, -40);
            cameraTarget = new Vector3(100, 20, 0);
            SetLookAtCamera(cameraPosition, cameraTarget, cameraUp);

        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!loaded)
                return;
        }


        Matrix4 matrixProjection, matrixModelview;
        float cameraRotation = 0f;
        float rotX = 0, rotY = 0, rotZ = 0;

        private void glControl1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //mousecoords();

        }

        void perp()
        {
            int w = glControl1.Width;
            int h = glControl1.Height;

            SetPerspectiveProjection(w, h, 45); // 45 is in degrees
            cameraPosition = new Vector3(0, 0, -40);
            cameraTarget = new Vector3(100, 20, 0);
            SetLookAtCamera(cameraPosition, cameraTarget, cameraUp);

        }

        void ortho()
        {
            int w = glControl1.Width;
            int h = glControl1.Height;

            projectionMatrix = Matrix4.Identity;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity(); // reset matrix
            GL.Ortho(0, w, 0, h, -10000, 10000);

        }
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();


            labelz();
            perp();

            translate();

            GL.Enable(EnableCap.DepthTest);

            GL.Color3(Color.Green);
            GL.Begin(BeginMode.Triangles);
            GL.Vertex3(-5, -5, 5);
            GL.Vertex3(5, -5, 5);
            GL.Vertex3(0, 5, -0);

            GL.Color3(Color.Blue);
            GL.Vertex3(5, -5, 5);
            GL.Vertex3(0, -5, -5);
            GL.Vertex3(0, 5, -0);

            GL.Color3(Color.Red);
            GL.Vertex3(-5, -5, 5);
            GL.Vertex3(0, -5, -5);
            GL.Vertex3(0, 5, -0);

            GL.End();



            ortho();
            GL.MatrixMode(MatrixMode.Modelview);


            GL.Begin(BeginMode.Triangles);
            GL.Color3(Color.White);
            GL.Vertex2(0, 10);
            GL.Vertex2(10, 10);
            GL.Vertex2(10, 0);

            GL.End();
            //catch()
            var mouse = Mouse.GetState();
            mousecoords(mouse.X, mouse.Y);


            label7.Text = mouse.X.ToString();
            label8.Text = mouse.Y.ToString();

            GL.VertexPointer(3, VertexPointerType.Float, 0, cube);
            GL.ColorPointer(4, ColorPointerType.Float, 0, cubeColors);
            GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedByte, triangles);

            glControl1.SwapBuffers();
            //glControl1.Invalidate();
        }
        void resize()
        {
            label11.Text = "abcdefgh";
            glControl1.Width = this.Width - 40;
            glControl1.Height = this.Height - 80;
            panel1.Location = new Point(this.Width - 150, 30);
            SetupViewport();
            perp();
            translateReset();
        }
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            resize();
        }
        void translateReset()
        {
            x = 0;
            y = 0;
            z = -50;
            rotX = 0;
            rotY = 0;
            rotZ = 0;

            glControl1.Invalidate();
        }

        private void onChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            continousToolStripMenuItem.Checked = false;
            onChangeToolStripMenuItem.Enabled = false;
            continousToolStripMenuItem.Enabled = true;
        }

        private void continousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onChangeToolStripMenuItem.Checked = false;
            onChangeToolStripMenuItem.Enabled = true;
            continousToolStripMenuItem.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            translateReset();

        }

        void labelz()
        {
            label1.Text = x.ToString();
            label2.Text = y.ToString();
            label3.Text = z.ToString();
        }
        void mousecoords(int mX, int mY)
        {

            var mouse = Mouse.GetState();

            int mX2 = mouse.X, mY2 = mouse.Y;

            if (mouse.IsButtonDown(MouseButton.Left) == true)
            {
                Thread.Sleep(1000 / 60);
                mouse = Mouse.GetState();


                label9.Text = mY.ToString();

                if (mX < mouse.X)
                {
                    rotY += 00000.1f;

                }
                if (mX > mouse.X)
                {
                    rotY -= 00000.1f;

                }

                if (mY < mouse.Y)
                {
                    rotX += 00000.1f;

                }
                if (mY > mouse.Y)
                {
                    rotX -= 00000.1f;

                }

                label10.Text = templicz.ToString();
                templicz++;
                translate();
                glControl1.Invalidate();
            }
            label10.Text = templicz.ToString();
            templicz++;
        }
        void translate()
        {
            modelViewMatrix = Matrix4.CreateRotationX(rotX) * Matrix4.CreateRotationY(rotY) * Matrix4.CreateRotationZ(rotZ) * Matrix4.CreateTranslation(new Vector3(x, y, z));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);
        }
        void translate(int xx, int yy, int zz)
        {
            modelViewMatrix = Matrix4.CreateRotationX(rotX) * Matrix4.CreateRotationY(rotY) * Matrix4.CreateRotationZ(rotZ) * Matrix4.CreateTranslation(new Vector3(xx, yy, zz));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);
        }
        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            int a;

            if (e.KeyCode == Keys.K)
                x += 1f;
            if (e.KeyCode == Keys.H)
                x -= 1f;

            if (e.KeyCode == Keys.U)
                y += 1f;
            if (e.KeyCode == Keys.J)
                y -= 1f;

            if (e.KeyCode == Keys.L)
                z += 1f;
            if (e.KeyCode == Keys.O)
                z -= 1f;

            if (e.KeyCode == Keys.Q)
                rotX += 0.1f;
            if (e.KeyCode == Keys.W)
                rotY += 0.1f;
            if (e.KeyCode == Keys.E)
                rotZ += 0.1f;
            if (e.KeyCode == Keys.Space)
                a = 0;

            glControl1.Invalidate();
        }
    }
}
