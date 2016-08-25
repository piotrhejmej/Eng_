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
using Eng_OpenTK.Rendering;


namespace Eng_OpenTK
{
    public partial class MainForm : Form
    {
        Setup setup = new Setup();
        Control control = new Control();

        bool loaded = false;
        bool continous = false;

        private Matrix4 projectionMatrix;
        private Matrix4 modelViewMatrix;
        private Vector3 cameraUp = Vector3.UnitY;

        float rotX = 0, rotY = 0, rotZ = 0;
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
            GL.ClearColor(0.11f, 0.11f, 0.11f, 0);
            GL.Enable(EnableCap.DepthTest);

            setup.SetupViewport(modelViewMatrix, projectionMatrix, glControl1.Width, glControl1.Height);
            resize();

        }
        

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!loaded)
                return;
        }
        
        
        

        
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            var mouse = Mouse.GetState();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();


            
            setup.SetupViewport(modelViewMatrix, projectionMatrix, glControl1.Width, glControl1.Height);

            translate();

            #region ostrosłup

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

            setup.OrthoView(projectionMatrix, glControl1.Width, glControl1.Height);

            GL.MatrixMode(MatrixMode.Modelview);


            GL.Begin(BeginMode.Triangles);
            GL.Color3(Color.White);
            GL.Vertex2(0, 10);
            GL.Vertex2(10, 10);
            GL.Vertex2(10, 0);

            GL.End();

            #endregion

            
            glControl1.SwapBuffers();

            labelz(mouse.X, mouse.Y);
            mousecoords(mouse.X, mouse.Y);
            Redraw();
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
            continous = false;
        }

        private void continousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onChangeToolStripMenuItem.Checked = false;
            onChangeToolStripMenuItem.Enabled = true;
            continousToolStripMenuItem.Enabled = false;
            continous = true;
            Redraw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            translateReset();
        }
               
        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            
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

            glControl1.Invalidate();
        }

        void resize()
        {
            label11.Text = "abcdefgh";
            glControl1.Width = this.Width - 170;
            glControl1.Height = this.Height - 80;
            panel1.Location = new Point(this.Width - 150, 30);
            panel1.Size = new Size(125, glControl1.Height - 3);
            setup.SetupViewport(modelViewMatrix, projectionMatrix, glControl1.Width, glControl1.Height);
            translateReset();
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
        void labelz(int mouseX, int mouseY)
        {
            label1.Text = x.ToString();
            label2.Text = y.ToString();
            label3.Text = z.ToString();
            label7.Text = mouseX.ToString();
            label8.Text = mouseY.ToString();
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
                    rotY += 00000.1f;

                if (mX > mouse.X)
                    rotY -= 00000.1f;

                if (mY < mouse.Y)
                    rotX += 00000.1f;

                if (mY > mouse.Y)
                    rotX -= 00000.1f;

                label10.Text = templicz.ToString();
                templicz++;
                translate();
                glControl1.Invalidate();
            }
            label10.Text = templicz.ToString();
            templicz++;
        }

        void Redraw()
        {
            if(continous == true)
                glControl1.Invalidate();
        }
    }
}
