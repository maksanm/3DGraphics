using Project4.ColorFilling;
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
    public class AbstractObject
    {
        public Vector3 Position { get; private set; }
        protected List<Vector3> points;
        protected List<List<int>> triangles = new List<List<int>>();
        protected Matrix modelMatrix = new Matrix(4, 4);
        protected Color color;
        protected double[,] zBuffer;

        public AbstractObject(Vector3 position, Color color, double[,] zBuffer)
        {
            this.zBuffer = zBuffer;
            this.Position = position;
            this.color = color;
            modelMatrix.matrix = new double[,] { { 1, 0, 0, position.X }, { 0, 1, 0, position.Y }, { 0, 0, 1, position.Z }, { 0, 0, 0, 1 } };
        }

        public void RotateX(int alpha)
        {
            modelMatrix.matrix[1, 1] = Math.Cos(alpha * Math.PI / 180);
            modelMatrix.matrix[1, 2] = -Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[2, 1] = Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[2, 2] = Math.Cos(alpha * Math.PI / 180);
        }

        public void RotateZ(int alpha)
        {
            modelMatrix.matrix[0, 0] = Math.Cos(alpha * Math.PI / 180);
            modelMatrix.matrix[0, 1] = -Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[1, 0] = Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[1, 1] = Math.Cos(alpha * Math.PI / 180);
        }

        public void RotateY(int alpha)
        {
            modelMatrix.matrix[0, 0] = Math.Cos(alpha * Math.PI / 180);
            modelMatrix.matrix[0, 2] = Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[2, 0] = -Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[2, 2] = Math.Cos(alpha * Math.PI / 180);
        }

        public void Translate(Vector3 offset)
        {
            Position = new Vector3(Position.X + offset.X, Position.Y + offset.Y, Position.Z + offset.Z);
            modelMatrix.matrix[0, 3] += offset.X;
            modelMatrix.matrix[1, 3] += offset.Y;
            modelMatrix.matrix[2, 3] += offset.Z;
        }

        public virtual void Draw(DirectBitmap canvas, Matrix projMatrix, Matrix viewMatrix)
        {
            Vector3[] pointsToDraw = new Vector3[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                Vector4 point = new Vector4(points[i].X, points[i].Y, points[i].Z, 1);
                point = modelMatrix * point;
                point = viewMatrix * point;
                point = projMatrix * point;
                pointsToDraw[i].X = point.X + 700;//спросить за нормализацию
                pointsToDraw[i].Y = point.Y + 400;
                pointsToDraw[i].Z = point.Z;
            }

            using Graphics g = Graphics.FromImage(canvas.Bitmap);
            foreach (var triangle in triangles)
            {
                var ba = pointsToDraw[triangle[0]] - pointsToDraw[triangle[1]];
                var ca = pointsToDraw[triangle[0]] - pointsToDraw[triangle[2]];
                if (Vector3.Cross(ba, ca).Z > 0)
                {
                    FillTriangle(canvas, pointsToDraw[triangle[0]], pointsToDraw[triangle[1]], pointsToDraw[triangle[2]]);
                    g.DrawLine(Pens.Black, new Point((int)pointsToDraw[triangle[0]].X, (int)pointsToDraw[triangle[0]].Y), new Point((int)pointsToDraw[triangle[1]].X, (int)pointsToDraw[triangle[1]].Y));
                    g.DrawLine(Pens.Black, new Point((int)pointsToDraw[triangle[0]].X, (int)pointsToDraw[triangle[0]].Y), new Point((int)pointsToDraw[triangle[2]].X, (int)pointsToDraw[triangle[2]].Y));
                    g.DrawLine(Pens.Black, new Point((int)pointsToDraw[triangle[1]].X, (int)pointsToDraw[triangle[1]].Y), new Point((int)pointsToDraw[triangle[2]].X, (int)pointsToDraw[triangle[2]].Y));
                }
            }
        }

        protected void FillTriangle(DirectBitmap canvas, Vector3 a, Vector3 b, Vector3 c)
        {
            List<Point> trianglePoints = new List<Point> { new Point((int)a.X, (int)a.Y), new Point((int)b.X, (int)b.Y), new Point((int)c.X, (int)c.Y) };
            var scanLine = new ScanLine(trianglePoints);
            foreach (var (xList, y) in scanLine.GetIntersectionPoints())
            {
                for (int i = 0; i < xList.Count - 1; i += 2)
                    for (int x = xList[i]; x < xList[i + 1]; x++)
                    {
                        Vector3 ab = b - a;
                        Vector3 ac = c - a;
                        Vector3 p = Vector3.Cross(ab, ac);
                        double z = a.Z - (p.X * (x - a.X) + p.Y * (y - a.Y)) / p.Z;
                        if (x < canvas.Width && x > 0 && y < canvas.Height && y > 0 && z >= zBuffer[x, y])
                        {
                            zBuffer[x, y] = z;
                            canvas.SetPixel(x, y, color);
                        }
                    }
            }

        }
    }
}
