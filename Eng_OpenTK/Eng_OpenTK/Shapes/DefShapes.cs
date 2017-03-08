using Eng_OpenTK.Cube;
using Eng_OpenTK.CubeFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eng_OpenTK.Shapes
{
    class DefShapes
    {
        IShape currentShape;
        int cB, cG, cR;

        public IShape getShape(int type, ref List<StateColorMemory> stateCollors, int x, int y, int z, int r, int baseAxis, int iteration, int hOrientation, int a, int h)
        {
            StateColorMemory tempColor = new StateColorMemory();
            Random rand = new Random();
            cB = rand.Next(100);
            cG = rand.Next(100);
            cR = rand.Next(100);

            if (type == 0)
            {
                Cuboid(x, y, z);
            }
            if (type == 1)
            {
                Cyllinder(x, y, z, baseAxis, r);
            }
            if (type == 2)
            {
                Sphere(r);
            }
            if (type == 3)
            {
                Wedge(x, y, z, baseAxis, r, hOrientation, a, h);
            }

            tempColor.state = iteration;
            tempColor.cellColor = currentShape.getColor();
            stateCollors.Add(tempColor);

            return currentShape;
        }

       
        void Cuboid(int x, int y, int z)
        {
            Cuboid cuboid = new Cuboid();

            cuboid.x = x;
            cuboid.y = y;
            cuboid.z = z;
            cuboid.startX = cuboid.startY = cuboid.startZ = 0;
            cuboid.color = ColourSetter.getColour(cR, cG, cB);

            currentShape = cuboid;
        }

        void Cyllinder(int x, int y, int z, int baseAxis, int r)
        {
            Cyllinder cyllinder = new Cyllinder();
            cyllinder.r = r;
            cyllinder.x = x;
            cyllinder.y = y;
            cyllinder.z = z;
            cyllinder.orientation = baseAxis;

            cyllinder.startX = cyllinder.startY = cyllinder.startZ = 0;
            cyllinder.color = ColourSetter.getColour(cR, cG, cB);

            currentShape = cyllinder;
        }
        void Sphere(int r)
        {
            Sphere sphere = new Sphere();
            sphere.r = r;

            sphere.startX = sphere.startY = sphere.startZ = 0;
            sphere.color = ColourSetter.getColour(cR, cG, cB);

            currentShape = sphere;
        }

        void Wedge(int x, int y, int z, int baseAxis, int r, int hOrientation, int a, int h)
        {
            Wedge wedge = new Wedge();
            wedge.x = x;
            wedge.y = y;
            wedge.z = z;
            wedge.r = r;
            wedge.a = a;
            wedge.h = h;
            wedge.hOrientation = hOrientation;
            wedge.orientation = baseAxis;

            wedge.startX = wedge.startY = wedge.startZ = 0;
            wedge.color = ColourSetter.getColour(cR, cG, cB);

            currentShape = wedge;
        }

    }
}
