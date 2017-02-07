using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eng_OpenTK.Cube
{
    class Assembly
    {
        public Cell assemblyCell(int X, int Y, int Z, int correction, float length, int count)
        {
            Cell cell = new Cell();
            cell.x = X;
            cell.y = Y;
            cell.z = Z;

            float x = length * (X - correction);
            float y = length * (Y - correction);
            float z = length * (Z - correction);
            cell.cellColor = ColourSetter.getColour(0, 0, 0);
            cell.state = 0;
            cell.cell = new float[]
            {
                x,              y,              z,
                x,              y - length,     z,
                x - length,     y - length,     z,
                x - length,     y,              z,
                x,              y,              z - length,
                x,              y - length,     z - length,
                x - length,     y - length,     z - length,
                x - length,     y,              z - length,
            };
            
            return cell;
        }


    }
}
