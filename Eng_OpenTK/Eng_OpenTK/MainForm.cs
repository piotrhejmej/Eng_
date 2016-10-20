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
using Eng_OpenTK.Cube;

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
        private int count = 85184;
        float size = 1f;

        private static List<Cube.Cube> cube = new List<Cube.Cube>();

        private static Assembly assembly = new Assembly();
        private static CubeRender cubeRender = new CubeRender();

        float rotX = 0, rotY = 0, rotZ = 0;
        float x = 0, y = 0, z = -50;

        int templicz = 0;

        


        public MainForm()
        {
            InitializeComponent();
            toolTip1.SetToolTip(panel2, "To set size of the matrix, enter down the size of the one axis and hit Set Size button.");
        }
        private void initialize()
        {
            int partialCount = (int)Math.Pow(count, 1.0f / 3.0f);
            int correction = partialCount / 2;
            Loader loader = new Loader();
            loader.Show();
            loader.setSize(count);

            for (int x = 0; x < partialCount; x++)
                for (int y = 0; y < partialCount; y++)
                    for (int z = 0; z < partialCount; z++)
                    {
                        assembly.buildCube(x-correction, y-correction, z-correction, size, count, ref cube);
                        loader.progres(x, y, z);
                    }
            loader.Close();
            loader.Dispose();
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

            //----------------------


            cubeRender.Render(cube, count);


            //---------------------

           
            setup.OrthoView(projectionMatrix, glControl1.Width, glControl1.Height);

            GL.MatrixMode(MatrixMode.Modelview);


            GL.Begin(BeginMode.Triangles);
            GL.Color3(Color.White);
            GL.Vertex2(0, 10);
            GL.Vertex2(10, 10);
            GL.Vertex2(10, 0);

            GL.End();

            //#endregion

            
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
            x = -45;
            y = -15;
            z = -150;
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
            glControl1.Width = this.Width - 250;
            glControl1.Height = this.Height - 80;
            panel1.Location = new Point(this.Width - 235, 30);
            panel1.Size = new Size(235, glControl1.Height - 3);
            setup.SetupViewport(modelViewMatrix, projectionMatrix, glControl1.Width, glControl1.Height);
            translateReset();
        }
        void translate()
        {
            modelViewMatrix = Matrix4.CreateRotationX(rotX) * Matrix4.CreateRotationY(rotY) * Matrix4.CreateRotationZ(rotZ) * Matrix4.CreateTranslation(new Vector3(x, y, z));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tempCount = 0;
            float tempSize = 0;
            int.TryParse(textBox1.Text, out tempCount);
            float.TryParse(textBox2.Text, out tempSize);
            count = (int)Math.Pow(tempCount, 3);
            size = 50f / tempCount;
            Console.Write("\n" + size + "count: " + tempCount);
            button2.Visible = false;
            textBox1.Visible = false;
            button3.Visible = true;

            label12.Text = "Click the Generate button to Generate and render matrix";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            initialize();
            button3.Visible = false;
            textBox1.Visible = false;
            button12.Visible = true;
            glControl1.Invalidate();


            label12.Text = "Click the Reset button to build new Matrix";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            button2.Visible = true;
            textBox1.Visible = true;
            button12.Visible = false;
            cube.Clear();


            label12.Text = "Matrix Generation. Choose size";
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
