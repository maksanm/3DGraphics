using Project4.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Objects
{
    public class Cube : AbstractObject
    {
        private int length, heigth, width;

        public Cube(Vector3 position, int length, int heigth, int width, Color color, double[,] zBuffer) : base(position, color, zBuffer)
        {
            this.length = length;
            this.heigth = heigth;
            this.width = width;
            this.points = new List<Vector3> { new Vector3(0, 0, 0), new Vector3(0, width, 0), new Vector3(length, width, 0), new Vector3(length, 0, 0),
                new Vector3(0, 0, heigth), new Vector3(0, width, heigth), new Vector3(length, width, heigth), new Vector3(length, 0, heigth)};
            CalculateTriangles();
        }

        private void CalculateTriangles()
        {
            triangles.AddRange(new List<List<int>>
            {
                new List<int> {0, 1, 4},
                new List<int> {4, 1, 5},

                new List<int> {3, 7, 2},
                new List<int> {7, 6, 2},

                new List<int> {1, 2, 5},
                new List<int> {5, 2, 6},

                new List<int> {0, 4, 3},
                new List<int> {4, 7, 3},

                new List<int> {0, 3, 1},
                new List<int> {3, 2, 1},

                new List<int> {4, 5, 7},
                new List<int> {7, 5, 6},
            });
        }
    }
}
