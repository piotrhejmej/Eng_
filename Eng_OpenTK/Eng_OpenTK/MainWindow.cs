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
using Eng_OpenTK.Shapes;
using Eng_OpenTK.GrainGrowth;
using System.Diagnostics;

namespace Eng_OpenTK
{
    public partial class MainWindow : Form
    {
        Setup setup = new Setup();
        static ValuesContainer valContainer = new ValuesContainer();

        IShape shape;
        List<Vector4> shapeList = new List<Vector4>();
        bool loaded = false;
        bool continous = false;

        private Matrix4 _projectionMatrix;
        private Matrix4 _modelViewMatrix;
        private Vector3 cameraUp = Vector3.UnitY;
        float size = 1f;
        int stateIterator = -1;
        Stopwatch timer = new Stopwatch();
        long fps = 0;

        private static List<Cube.Cell> cells = new List<Cube.Cell>();
        private static List<StateColorMemory> stateCollors = new List<StateColorMemory>();

        private static Assembly assembly = new Assembly();
        private static CellRender cellRender = new CellRender();

        float rotX = 0, rotY = 0, rotZ = 0;
        float x = 0, y = 0, z = -50;

        int templicz = 0;
        
        public MainWindow()
        { 
            InitializeComponent();
            InitializationWindow initWindow = new InitializationWindow();
            this.Opacity = 0;
            initWindow.Show(this);
        }
        public void setVars(int count)
        {
            int temp = (int)Math.Pow(count, 3);
            valContainer.setCount(temp);
            valContainer.setX(count);
            valContainer.setY(count);
            valContainer.setZ(count);
            trackBar1.Maximum = count;
            trackBar2.Maximum = count;
            trackBar3.Maximum = count;

            trackBar1.Value = count;
            trackBar2.Value = count;
            trackBar3.Value = count;

            size = 50f / count;

            Initialize();
        }
        private void Initialize()
        {
            int partialCount = (int)Math.Pow(valContainer.getCount(), 1.0f / 3.0f);
            int correction = partialCount / 2;
            Loader loader = new Loader();
            loader.Show();
            loader.setSize(valContainer.getCount());

            for (int x = 0; x < partialCount; x++)
                for (int y = 0; y < partialCount; y++)
                    for (int z = 0; z < partialCount; z++)
                    {
                        cells.Add(assembly.assemblyCell(x, y, z, correction, size, valContainer.getCount()));
                        loader.progres(x, y, z, partialCount);
                    }
            
            Console.Write("\n");
            loader.Close();
            loader.Dispose();
            valContainer.enableDrawGrainsState();
            valContainer.enableDrawShapesState();
        }
        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            GL.ClearColor(0.11f, 0.11f, 0.11f, 0);
            GL.Enable(EnableCap.DepthTest);

            setup.SetupViewport(_modelViewMatrix, _projectionMatrix, glControl1.Width, glControl1.Height);
            resize();
        }
        private void glControl1_Resize(object sender, EventArgs e)
        {
            setup.SetupViewport(_modelViewMatrix, _projectionMatrix, glControl1.Width, glControl1.Height);
            if (!loaded)
                return;
        }
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            
            timer.Start();

            var mouse = Mouse.GetState();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            
            translate();
            int empty = 0;
            int notEmpty = 0;
            
            try
            {
                foreach (Cube.Cell item in cells)
                {
               
                        if (item.state == 0)
                            empty++;
                        else
                            notEmpty++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Thread.Sleep(500);
            }

            if (empty == 0 && notEmpty != 0)
                valContainer.setFull();

            if (cells.Any())
                cellRender.Render(cells, valContainer);
            
            glControl1.SwapBuffers();

            labelz(mouse.X, mouse.Y);            
            Redraw(mouse.X, mouse.Y);
            
            fps++;
            if (timer.ElapsedMilliseconds > 1000)
            {
                label10.Text = fps.ToString();
                timer.Stop();
                timer.Reset();
                fps = 0;
            }
            

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
            label10.Visible = false;
            label16.Visible = false;
        }

        private void continousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onChangeToolStripMenuItem.Checked = false;
            onChangeToolStripMenuItem.Enabled = true;
            continousToolStripMenuItem.Enabled = false;
            continous = true;
            label10.Visible = true;
            label16.Visible = true;
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
            glControl1.Width = this.Width - 250;
            glControl1.Height = this.Height - 80;
            panel1.Location = new Point(this.Width - 235, 30);
            panel1.Size = new Size(235, glControl1.Height - 3);
            setup.SetupViewport(_modelViewMatrix, _projectionMatrix, glControl1.Width, glControl1.Height);
            translateReset();
        }
        void translate()
        {
            _modelViewMatrix = Matrix4.CreateRotationX(rotX) * Matrix4.CreateRotationY(rotY) * Matrix4.CreateRotationZ(rotZ) * Matrix4.CreateTranslation(new Vector3(x, y, z));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref _modelViewMatrix);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tempCount = 0;
            valContainer.setCount((int)Math.Pow(tempCount, 3));
            size = 50f / tempCount;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Initialize();
            glControl1.Invalidate();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            cells.Clear();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

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
                        foreach (Cube.Cell item in cells)
                        {
                            outfile.WriteLine(String.Format("{0} {1} {2} {3}", item.x, item.y, item.z, item.state));
                        }
                    }
                }
            }
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
                cells.Clear();
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            ShapeDefinition shapeDef = new ShapeDefinition();
            shapeDef.Show(this);
        }
        public void defShape(int x, int y, int z, int whichShape, int baseAxis, int r)
        {
            stateIterator--;
            DefShapes Definitor = new DefShapes();

            shape = Definitor.getShape(whichShape, ref stateCollors, x, y, z, r, baseAxis, stateIterator);          
            
            moveShape();
        }
        void changeStateInList(ref Vector4 item, int state)
        {
            item.W = state;
        }
        private void moveShape()
        {
            ColourSetter setter = new ColourSetter();
            shapeList.Clear();
            int partialCount = (int)Math.Pow(valContainer.getCount(), 1.0f / 3.0f);
            
            shapeList = shape.returnShapeCoords(ref cells, ref valContainer);
            
            foreach(Vector4 item in shapeList)
            {
                int cubeCoord = (int)(item.X * partialCount * partialCount + item.Y * partialCount + item.Z);
                cells[cubeCoord].prevState = cells[cubeCoord].state;
                cells[cubeCoord].state = stateIterator;
                cells[cubeCoord].prevColor = cells[cubeCoord].cellColor;
                cells[cubeCoord].cellColor = setter.getColour(shape.getColor());
            }
            glControl1.Invalidate();
        }
        
        private void clearStates()
        {
            ColourSetter setter = new ColourSetter();

            int partialCount = (int)Math.Pow(valContainer.getCount(), 1.0f / 3.0f);
            foreach (Vector4 item in shapeList)
            {
                int cubeCoord = (int)(item.X * partialCount * partialCount + item.Y * partialCount + item.Z);
                cells[cubeCoord].state = cells[cubeCoord].prevState;
                cells[cubeCoord].cellColor = cells[cubeCoord].prevColor;
            }
            
            glControl1.Invalidate();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.moveY(true);
            moveShape();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.moveY(false);
            moveShape();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.moveX(false);
            moveShape();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.moveX(true);
            moveShape();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.moveZ(false);
            moveShape();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            clearStates();
            shape.moveZ(true);
            moveShape();
        }
        void translate(int xx, int yy, int zz)
        {
            _modelViewMatrix = Matrix4.CreateRotationX(rotX) * Matrix4.CreateRotationY(rotY) * Matrix4.CreateRotationZ(rotZ) * Matrix4.CreateTranslation(new Vector3(xx, yy, zz));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref _modelViewMatrix);
        }
        private void button13_Click(object sender, EventArgs e)
        {
            stateIterator++;
            StateComputing.fillSolidState(ref cells, stateIterator);
            glControl1.Invalidate();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            SeedGrowth grower = new SeedGrowth();
            Thread growerThread = new Thread(() => grower.grainGrowth(ref cells, stateCollors, valContainer, this));
            growerThread.Start();

            Console.WriteLine("IM OUT OF GROWTH");
            glControl1.Invalidate();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            int x, y, z;
            int.TryParse(textBox1.Text, out x);
            int.TryParse(textBox2.Text, out y);
            int.TryParse(textBox3.Text, out z);

            shape.setPos(x, y, z);
            clearStates();
            moveShape();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
                int ile = 100;
                int prev = 0;
                Random rand = new Random();
            int baseAxis = 2;//rand.Next(3);
                double percentageFillage = 0;
                int volume = valContainer.getCount();
          //  RandomShapeGenerator abd = new RandomShapeGenerator();
          //  abd.Show();


            for (int i = 0; percentageFillage < (0.01 * volume); i++)
                {

                int type = 0; // rand.Next(3);
                int max = (int)Math.Pow(valContainer.getCount(), 1.0f / 3.0f);

                Thread.Sleep(10);
                int x = rand.Next(max - (int)0.05 * max) + (int)0.05 * max;
                Thread.Sleep(10);
                int y = rand.Next(max-(int)0.05*max)+ (int)0.05 * max;
                Thread.Sleep(10);
                int z = rand.Next(max - (int)0.05 * max) + (int)0.05 * max;
                Thread.Sleep(10);
                int r = rand.Next(max/10);
                Thread.Sleep(10);
                int movX = rand.Next(max*2)-max;
                Thread.Sleep(10);
                int movY = rand.Next(max * 2) - max;
                Thread.Sleep(10);
                int movZ = rand.Next(max * 2) - max;

                if (prev == x + y)
                {
                    i--;
                    continue;
                }
                prev = x + y;
                
                if (type == 0)
                {
                    x = (int)(x * 0.15);
                    y = (int)(y * 0.15);
                    z = (int)(z * 0.15);

                    percentageFillage += x * y * z;
                }


                if (type == 1)
                {
                    if (baseAxis == 0)
                    {
                        x = 2 * r;
                        y = 2 * r;
                        percentageFillage += 3.14* (r * r) * z;
                    }
                    if (baseAxis  == 1)
                    {
                        z = 2 * r;
                        x = 2 * r;
                        percentageFillage += 3.14 * (r * r) * y;
                    }
                    if (baseAxis == 2)
                    {
                        z = 2 * r;
                        y = 2 * r;
                        percentageFillage += 3.14 * (r * r) * x;
                    }
                }
                if (type == 2)
                {
                    x = 2 * r;
                    y = 2 * r;
                    z = 2 * r;
                    percentageFillage += (4 / 3) * 3.14 * (r * r * r);
                }

                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6} mov: {7}, {8}, {9}", x, y, z, r, type, baseAxis, max, movX, movY, movZ);
                
                defShape(x, y, z, type, baseAxis, r);
                shape.setPos(movX, movY, movZ);
                clearStates();
                moveShape();
            }
            
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            Random rand = new Random();
            
            int max = (int)Math.Pow(valContainer.getCount(), 1.0f / 3.0f);
            int grainState = 1;
            int cB, cG, cR;
            for (int i=0;i<10;i++)
            {
                cB = rand.Next(100);
                cG = rand.Next(100);
                cR = rand.Next(100);

                int x = rand.Next(max);
                Thread.Sleep(10);
                int y = rand.Next(max);
                Thread.Sleep(10);
                int z = rand.Next(max);

                int iterator = (x * max * max + y * max + z);

                if (cells[iterator].state == 0)
                {
                    StateColorMemory tempColor = new StateColorMemory();
                    cells[iterator].state = returnSame(grainState);
                    cells[iterator].cellColor = ColourSetter.getColour(cR, cG, cB);

                    tempColor.state = returnSame(grainState);
                    tempColor.cellColor = ColourSetter.getColour(cR, cG, cB);
                    stateCollors.Add(tempColor);
                }
                else
                {
                    grainState--;
                    i--;
                }
                
                
                grainState++;
            }
        }
        int returnSame(int dupa)
        {
            return dupa;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            valContainer.setZ(trackBar1.Value);
            glControl1.Invalidate();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            valContainer.setX(trackBar3.Value);
            glControl1.Invalidate();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            valContainer.setY(trackBar2.Value);
            glControl1.Invalidate();
        }

        void labelz(int mouseX, int mouseY)
        {
            label1.Text = x.ToString();
            label2.Text = y.ToString();
            label3.Text = z.ToString();
            label7.Text = mouseX.ToString();
            label8.Text = mouseY.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                valContainer.enableDrawGrainsState();
            else
                valContainer.disableDrawGrainsState();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                valContainer.enableDrawShapesState();
            else
                valContainer.disableDrawShapesState();
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


                templicz++;
                translate();
            }
            templicz++;
        }
        
        public void Redraw(int mouseX, int mouseY)
        {
            if(continous == true)
            {
                glControl1.Invalidate();
                mousecoords(mouseX, mouseY);
            }
        }
        public void Redraw()
        {
            if (continous == true)
            {
                glControl1.Invalidate();
            }
        }
        public int getPartialCount()
        {
            return (int)Math.Pow(valContainer.getCount(), 1.0f / 3.0f);
        }
    }
}
