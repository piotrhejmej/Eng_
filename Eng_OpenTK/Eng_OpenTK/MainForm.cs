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
using Eng_OpenTK.CubeFiles;
using System.IO;

namespace Eng_OpenTK
{
    public partial class MainForm : Form
    {
        Setup setup = new Setup();
        static Controll control = new Controll();
        Shape shape = new Shape();
        List<Vector3> coordList = new List<Vector3>();
        List<Vector3> shapeList = new List<Vector3>();

        bool loaded = false;
        bool continous = false;

        private Matrix4 projectionMatrix;
        private Matrix4 modelViewMatrix;
        private Vector3 cameraUp = Vector3.UnitY;
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
            InitializationWindow initWindow = new InitializationWindow();
            this.Opacity = 0;
            initWindow.Show(this);
        }
        public void setVars(int count)
        {
            control.setCount((int)Math.Pow(count, 3));
            size = 50f / count;

            initialize();
            glControl1.Invalidate();
        }
        private void initialize()
        {
            int partialCount = (int)Math.Pow(control.getCount(), 1.0f / 3.0f);
            int correction = partialCount / 2;
            Loader loader = new Loader();
            loader.Show();
            loader.setSize(control.getCount());

            for (int x = 0; x < partialCount; x++)
                for (int y = 0; y < partialCount; y++)
                    for (int z = 0; z < partialCount; z++)
                    {
                        cube.Add(assembly.buildCube(x, y, z, correction, size, control.getCount()));
                        loader.progres(x, y, z, partialCount);
                    }
            
            Console.Write("\n");
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

            if (cube.Any())
                cubeRender.Render(cube, control.getCount());

            setup.OrthoView(projectionMatrix, glControl1.Width, glControl1.Height);

            GL.MatrixMode(MatrixMode.Modelview);

            GL.Begin(BeginMode.Triangles);
            GL.Color3(Color.White);
            GL.Vertex2(0, 10);
            GL.Vertex2(10, 10);
            GL.Vertex2(10, 0);

            GL.End();

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
            x = -15;
            y = 3;
            z = -95;
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
            label11.Text = "Hello Moto";
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
            control.setCount((int)Math.Pow(tempCount, 3));
            size = 50f / tempCount;
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            initialize();
            button3.Visible = false;
            button12.Visible = true;
            glControl1.Invalidate();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Visible = false;
            cube.Clear();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string line;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    myStream.Close();
                    using (StreamWriter outfile = new StreamWriter(saveFileDialog1.FileName))
                    {
                        foreach (Cube.Cube item in cube)
                        {
                            outfile.WriteLine(String.Format("{0}, {1}, {2}, {3}", item.x, item.y, item.z, item.state));
                        }
                    }
                }
            }
            Console.WriteLine("I'm out biatch");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to start New instance? Any unsaved work will be lost", "Are you Sure?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Console.WriteLine("New Instance");
                this.Opacity = 0;
                InitializationWindow initWin = new InitializationWindow();
                initWin.Show(this);
                cube.Clear();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ShapeDefinition shapeDef = new ShapeDefinition();
            shapeDef.Show(this);
        }

        public void defShape(int x, int y, int z)
        {
            shape.x = x;
            shape.y = y;
            shape.z = z;
            shape.startX = shape.startY = shape.startZ = 0;
            
            drawShape();
        }
        private List<Vector3> returnShapeCoords(Vector3 startPoint, Vector3 endPoint)
        {
            Vector3 coord = new Vector3(startPoint.X,startPoint.Y,startPoint.Z);
            int partialCount = (int)Math.Pow(control.getCount(), 1.0f / 3.0f);

            float xx, yy, zz;

            for (float x = startPoint.X; x < endPoint.X + startPoint.X; x++)
                for (float y = startPoint.Y; y < endPoint.Y + startPoint.Y; y++)
                    for (float z = startPoint.Z; z < endPoint.Z + startPoint.Z; z++)
                    {
                        xx = x;
                        yy = y;
                        zz = z;

                        shapeBoudaries(ref xx, ref yy, ref zz, partialCount);
                        coordList.Add(new Vector3(xx, yy, zz));
                    }
           return coordList;
        }
        private void drawShape()
        {
            ColourSetter setter = new ColourSetter();
            shapeList.Clear();
            int partialCount = (int)Math.Pow(control.getCount(), 1.0f / 3.0f);

            shapeList = returnShapeCoords(new Vector3(shape.startX, shape.startY, shape.startZ), new Vector3(shape.x, shape.y, shape.z));
            
            foreach(Vector3 item in shapeList)
            {
                cube[(int)(item.X * partialCount * partialCount + item.Y * partialCount + item.Z)].state = 1;
                cube[(int)(item.X * partialCount * partialCount + item.Y * partialCount + item.Z)].cubeColor = setter.getColour(125);
            }

            glControl1.Invalidate();
        }
        
        private void clearStates()
        {
            float xx, yy, zz;
            int partialCount = (int)Math.Pow(control.getCount(), 1.0f / 3.0f);
            for (int x = shape.startX; x < shape.x + shape.startX; x++)
                for (int y = shape.startY; y < shape.y + shape.startY; y++)
                    for (int z = shape.startZ; z < shape.z + shape.startZ; z++)
                    {
                        xx = x;
                        yy = y;
                        zz = z;

                        shapeBoudaries(ref xx, ref yy, ref zz, partialCount);
                        
                        try
                        {
                            cube[(int)(xx * partialCount * partialCount + yy * partialCount + zz)].state = 0;
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("An error occured: \n" + e);
                        }
                    }
            glControl1.Invalidate();
        }
        private void shapeBoudaries(ref float x, ref float y, ref float z, int partialCount)
        {
            if (x < 0)
                x = (partialCount - Math.Abs(x % partialCount)) - 1;
            else if (x >= partialCount)
                x = (Math.Abs(x % partialCount));

            if (y < 0)
                y = (partialCount - Math.Abs(y % partialCount)) - 1;
            else if (y >= partialCount)
                y = (Math.Abs(y % partialCount));

            if (z < 0)
                z = (partialCount - Math.Abs(z % partialCount)) - 1;
            else if (z >= partialCount)
                z = (Math.Abs(z % partialCount));

        }
        private void button4_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.startY++;
            drawShape();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.startY--;
            drawShape();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.startX--;
            drawShape();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.startX++;
            drawShape();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.startZ--;
            drawShape();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.startZ++;
            drawShape();
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
        public int getPartialCount()
        {
            return (int)Math.Pow(control.getCount(), 1.0f / 3.0f);
        }
    }
}
