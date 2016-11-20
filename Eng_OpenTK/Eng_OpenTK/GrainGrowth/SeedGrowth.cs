using Eng_OpenTK.CubeFiles;
using Eng_OpenTK.Rendering;
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
        public void grainGrowth(ref List<Cube.Cube> cells, List<StateColorMemory> collors, Controll control)
        {
            List<Cube.Cube> tempCells = new List<Cube.Cube>();
            Stopwatch stopWatch = new Stopwatch();
            double watchStart, watchStop, watchElapsed;
            
            foreach(Cube.Cube item in cells)
            {
                Cube.Cube temp = new Cube.Cube();

                temp.state = item.state;
                temp.cellColor = item.cellColor;
                temp.cell = item.cell;
                temp.x = item.x;
                temp.y = item.y;
                temp.z = item.z;
                temp.prevColor = item.prevColor;
                temp.prevState = item.prevState;

                tempCells.Add(temp);
            }

            int partialCount = (int)Math.Pow(control.getCount(), 1.0f / 3.0f);
            tempCells[0].state = 50;
            stopWatch.Start();
            int it = 0;
            while(true)
            {
                stopWatch.Reset();
                stopWatch.Start();
                it++;
                for (int x = 0; x < partialCount; x++)
                    for (int y = 0; y < partialCount; y++)
                        for (int z = 0; z < partialCount; z++)
                        {
                            int cubeCoord = (int)(x * partialCount * partialCount + y * partialCount + z);
                            if (tempCells[cubeCoord].state == 0)
                                neighbourhood(ref cells, ref tempCells, collors, control, x, y, z);
                        }

                int diff = 0;
                for(int o = 0; o < control.getCount(); o++)
                {
                    if (cells[o].state != tempCells[o].state)
                        diff++;
                }

                if (cells == tempCells || diff == 0)
                    break;

                cells.Clear();

                foreach (Cube.Cube item in tempCells)
                {
                    Cube.Cube temp = new Cube.Cube();

                    temp.state = item.state;
                    temp.cellColor = item.cellColor;
                    temp.cell = item.cell;
                    temp.x = item.x;
                    temp.y = item.y;
                    temp.z = item.z;
                    temp.prevColor = item.prevColor;
                    temp.prevState = item.prevState;

                    cells.Add(temp);
                }

                stopWatch.Stop();
                Console.WriteLine("still in the loop :) {0}: {1}", it, stopWatch.ElapsedMilliseconds);
            }     
        }
        
        void neighbourhood(ref List<Cube.Cube> cells, ref List<Cube.Cube> tempCells, List<StateColorMemory> collors, Controll control, int xx, int yy, int zz)
        {
            int partialCount = (int)Math.Pow(control.getCount(), 1.0f / 3.0f);

            for (int x = xx - 1; x <= xx + 1; x++)
                for (int y = yy - 1; y <= yy + 1; y++)
                    for (int z = zz - 1; z <= zz + 1; z++)
                    {
                        

                        /*
                            if (x == (xx - 1))
                            {
                                if ((y == (yy - 1)) || (y == (yy + 1)))
                                {
                                    continue;
                                }
                                if((z == (zz-1)) && (z == (zz +1)))
                                {
                                    continue;
                                }
                            }
                            if (x == (xx + 1))
                            {
                                if ((y == (yy - 1)) || (y == (yy + 1)))
                                {
                                    continue;
                                }
                                if ((z == (zz - 1)) && (z == (zz + 1)))
                                {
                                    continue;
                                }
                        }
                        */

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
                        int temp = neighbourCount(ref cells, control, tempX, tempY, tempZ);

                        tempCells[cubeCoord].state = temp;
                        

                        foreach(StateColorMemory item in collors)
                        {
                            if (item.state == temp)
                                tempCells[cubeCoord].cellColor = item.cellColor;
                        }

                    }



        }

        int neighbourCount(ref List<Cube.Cube> cells, Controll control, int xx, int yy, int zz)
        {
            int resultState = 0 ;
            int partialCount = (int)Math.Pow(control.getCount(), 1.0f / 3.0f);
            int[] counter = new int[partialCount];
            

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
                        if (cells[cubeCoord].state != 0)
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
