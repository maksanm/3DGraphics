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
    public class Sphere : AbstractObject
    {
        private int radius;
        private int nPol = 10;
        private int nAng = 10;

        public Sphere(Vector3 position, int radius, Color color, double[,] zBuffer) : base(position, color, zBuffer)
        {
            this.radius = radius;
            points = GetVertices();
            CalculateTriangles();
            GetTrianglesNormals();
        }

        private List<Vector3> GetVertices()
        {
            List<Vector3> result = new List<Vector3>();
            for(int i = 0; i < nPol; i++)
                for(int j = 0; j <= nAng; j++)
                {
                    Vector3 newPoint = new Vector3();
                    newPoint.X = (float)(Math.Cos(2 * Math.PI * j / nAng) * radius * i / (nPol - 1));
                    newPoint.Y = (float)(Math.Sin(2 * Math.PI * j / nAng) * radius * i / (nPol - 1));
                    newPoint.Z = (float)(Math.Sqrt(Math.Max(0 , radius * radius - newPoint.X * newPoint.X - newPoint.Y * newPoint.Y)));
                    result.Add(newPoint);
                }
            for (int i = 0; i < nPol; i++)
                for (int j = 0; j <= nAng; j++)
                {
                    Vector3 newPoint = new Vector3();
                    newPoint.X = -(float)(Math.Cos(2 * Math.PI * j / nAng) * radius * i / (nPol - 1));
                    newPoint.Y = -(float)(Math.Sin(2 * Math.PI * j / nAng) * radius * i / (nPol - 1));
                    newPoint.Z = -(float)(Math.Sqrt(Math.Max(0, radius * radius - newPoint.X * newPoint.X - newPoint.Y * newPoint.Y)));
                    result.Add(newPoint);
                }
            return result;
        }

        private void CalculateTriangles()
        {
            for(int i = 0; i < nPol - 1; i++)
                for(int j = 0; j < nAng; j++)
                {
                    triangles.Add(new List<int> { i * (nAng + 1) + j, i * (nAng + 1) + j + 1, (i + 1) * (nAng + 1) + j });
                    triangles.Add(new List<int> { (i + 1) * (nAng + 1) + j + 1, (i + 1) * (nAng + 1) + j, i * (nAng + 1) + j + 1 });
                }
            for (int i = nPol; i < 2 * nPol - 1; i++)
                for (int j = 0; j < nAng; j++)
                {
                    triangles.Add(new List<int> { i * (nAng + 1) + j, (i + 1) * (nAng + 1) + j, i * (nAng + 1) + j + 1 });
                    triangles.Add(new List<int> { (i + 1) * (nAng + 1) + j + 1, i * (nAng + 1) + j + 1, (i + 1) * (nAng + 1) + j });
                }
        }
    }
}
