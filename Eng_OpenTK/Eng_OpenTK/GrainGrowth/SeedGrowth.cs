using Eng_OpenTK.CubeFiles;
using Eng_OpenTK.Rendering;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eng_OpenTK.GrainGrowth
{
    class SeedGrowth
    {
        static void cloneLists(List<Cube.Cell> input, out List<Cube.Cell> output)
        {
            List<Cube.Cell> tempList = new List<Cube.Cell>();

            foreach (Cube.Cell item in input)
            {
                Cube.Cell tempItem = new Cube.Cell();

                tempItem.state = item.state;
                tempItem.cellColor = item.cellColor;
                tempItem.cell = item.cell;
                tempItem.x = item.x;
                tempItem.y = item.y;
                tempItem.z = item.z;
                tempItem.prevColor = item.prevColor;
                tempItem.prevState = item.prevState;

                tempList.Add(tempItem);
            }
            output = tempList;
        }
        static bool compareLists(List<Cube.Cell> a, List<Cube.Cell> b, ValuesContainer control)
        {
            int diff = 0;
            for (int o = 0; o < control.getCount(); o++)
            {
                if (a[o].state != b[o].state)
                    diff++;
            }

            if (a != b && diff == 0)
                return true;
            else
                return false;

        }
        public void grainGrowth(ref List<Cube.Cell> cells, List<StateColorMemory> collors, ValuesContainer control, MainWindow parent)
        {
            List<Cube.Cell> tempCells = new List<Cube.Cell>();
            Stopwatch stopWatch = new Stopwatch();
            
            cloneLists(cells, out tempCells);

            int partialCount = (int)Math.Pow(control.getCount(), 1.0f / 3.0f);
            stopWatch.Start();
            int it = 0;

            while(true)
            {
                stopWatch.Reset();
                stopWatch.Start();

                foreach (Cube.Cell item in tempCells)
                {
                    if (item.state == 0)
                        neighbourhood(ref cells, ref tempCells, collors, control, item.x, item.y, item.z);
                }
                               

                if ((compareLists(cells, tempCells, control)))
                {
                    break;
                }

                cells.Clear();
                cloneLists(tempCells, out cells);

                stopWatch.Stop();
                Console.WriteLine("Grain Growth iteration {0}: elapsed time: {1}", it++, stopWatch.ElapsedMilliseconds);
                parent.glControl1.Invalidate();
            }   
        }
        
        void neighbourhood(ref List<Cube.Cell> cells, ref List<Cube.Cell> tempCells, List<StateColorMemory> collors, ValuesContainer control, int xx, int yy, int zz)
        {
            int partialCount = (int)Math.Pow(control.getCount(), 1.0f / 3.0f);

            for (int x = xx - 1; x <= xx + 1; x++)
                for (int y = yy - 1; y <= yy + 1; y++)
                    for (int z = zz - 1; z <= zz + 1; z++)
                    {
                        int tempX = x;
                        int tempY = y;
                        int tempZ = z;

                        if (x < 0)
                            tempX = (partialCount - Math.Abs(x % partialCount)) - 1;
                        else if (x >= partialCount)
                            tempX = (Math.Abs(x % partialCount));

                        if (y < 0)
                            tempY = (partialCount - Math.Abs(y % partialCount)) - 1;
                        else if (y >= partialCount)
                            tempY = (Math.Abs(y % partialCount));

                        if (z < 0)
                            tempZ = (partialCount - Math.Abs(z % partialCount)) - 1;
                        else if (z >= partialCount)
                            tempZ = (Math.Abs(z % partialCount));


                        int cubeCoord = (int)(tempX * partialCount * partialCount + tempY * partialCount + tempZ);

                        if (tempCells[cubeCoord].state == 0)
                        {
                            int temp = neighbourCount(ref cells, control, tempX, tempY, tempZ);

                            tempCells[cubeCoord].state = temp;


                            foreach (StateColorMemory item in collors)
                            {
                                if (item.state == temp)
                                    tempCells[cubeCoord].cellColor = item.cellColor;
                            }
                        }
                    }



        }

        int neighbourCount(ref List<Cube.Cell> cells, ValuesContainer control, int xx, int yy, int zz)
        {
            int resultState = 0 ;
            int partialCount = (int)Math.Pow(control.getCount(), 1.0f / 3.0f);
            int[] counter = new int[500];
            

            for (int i = xx - 1; i <= xx + 1; i++)
            {
                for (int j = yy - 1; j <= yy + 1; j++)
                {
                    for (int k = zz - 1; k <= zz + 1; k++)
                    {
                        
                        if (i == xx && j == yy && k == zz)
                        {
                            continue;
                        }
                        int tempX = i;
                        int tempY = j;
                        int tempZ = k;


                        if (i < 0)
                            tempX = (partialCount - Math.Abs(i % partialCount)) - 1;
                        else if (i >= partialCount)
                            tempX = (Math.Abs(i % partialCount));

                        if (j < 0)
                            tempY = (partialCount - Math.Abs(j % partialCount)) - 1;
                        else if (j >= partialCount)
                            tempY = (Math.Abs(j % partialCount));

                        if (k < 0)
                            tempZ = (partialCount - Math.Abs(k % partialCount)) - 1;
                        else if (k >= partialCount)
                            tempZ = (Math.Abs(k % partialCount));

                        int cubeCoord = (tempX * partialCount * partialCount + tempY * partialCount + tempZ);
                        if (cells[cubeCoord].state > 0)
                        {
                            counter[cells[cubeCoord].state]++;
                        }
                    }
                }
            }
            int max = counter[0];
            for (int k = 1; k < partialCount; k++)
            {
                if (counter[k] > max)
                {
                    max = counter[k];
                    resultState = k;
                }
            }
            int ileMax = 0;
            for (int k = 0; k < partialCount; k++)
            {
                if (counter[k] == max)
                {
                    ileMax++;
                }
            }
            if (ileMax != 1)
            {
                int[] temp1 = new int[ileMax];
                int iter = 0;
                for (int k = 0; k < partialCount; k++)
                {
                    if (counter[k] == max)
                    {
                        temp1[iter++] = k;
                    }
                }
                if(max != 0)
                {
                    Random rand = new Random();
                    resultState = temp1[rand.Next(ileMax)];
                }
            }
            return resultState;
        }

    }

}
