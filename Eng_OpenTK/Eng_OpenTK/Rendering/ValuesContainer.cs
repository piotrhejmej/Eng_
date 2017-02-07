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
    class ValuesContainer
    {
        private int count { get; set; }
        private bool full = false;
        private int x, y, z;
        private bool drawShapes, drawGrains;

        
        public void enableDrawGrainsState()
        {
            drawGrains = true;
        }
        public void disableDrawGrainsState()
        {
            drawGrains = false;
        }
        public void enableDrawShapesState()
        {
            drawShapes = true;
        }
        public void disableDrawShapesState()
        {
            drawShapes = false;
        }
        public bool getDrawShapeState()
        {
            return drawShapes;
        }
        public bool getDrawGrainsState()
        {
            return drawGrains;
        }
        public void setVariables(int Count)
        {
            count = Count;
        }
        public int getCount()
        {
            return count;
        }
        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;

        }
        public int getZ()
        {
            return z;
        }
        public void setX(int X)
        {
            x = X;
        }
        public void setY(int Y)
        {
            y = Y; ;
        }
        public void setZ(int Z)
        {
            z = Z;
        }
        public bool isFull()
        {
            return full;
        }
        public void setFull()
        {
            full = true;
        }
        public void setEmpty()
        {
            full = false;
        }
        public void setCount(int Count)
        {
            count = Count;
        }
    }
}
