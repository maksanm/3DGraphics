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
        protected List<Vector3> trianglesNormals;
        protected Matrix modelMatrix = new Matrix(4, 4);
        protected Matrix normalMatrix = new Matrix(4, 4);
        protected Color color;
        protected double[,] zBuffer;
        protected ColorGenerator colorGenerator = null;

        public AbstractObject(Vector3 position, Color color, double[,] zBuffer)
        {
            this.zBuffer = zBuffer;
            this.Position = position;
            this.color = color;
            modelMatrix.matrix = new double[,] { { 1, 0, 0, position.X }, { 0, 1, 0, position.Y }, { 0, 0, 1, position.Z }, { 0, 0, 0, 1 } };
        }

        protected void GetTrianglesNormals()
        {
            trianglesNormals = new List<Vector3>();
            foreach (var triangle in triangles)
            {
                var ab = points[triangle[1]] - points[triangle[0]];
                var ac = points[triangle[2]] - points[triangle[0]];
                var normal = Vector3.Cross(ab, ac);
                trianglesNormals.Add(Vector3.Normalize(normal));
            }
        }

        public virtual void AddLights(float ka, float kd, float ks, int n, List<LightSource> lightSources)
        {
            colorGenerator = new ColorGenerator(color, ka, kd, ks, n, lightSources);
        }

        public virtual void RotateX(int alpha)
        {
            modelMatrix.matrix[1, 1] = Math.Cos(alpha * Math.PI / 180);
            modelMatrix.matrix[1, 2] = -Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[2, 1] = Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[2, 2] = Math.Cos(alpha * Math.PI / 180);
        }

        public virtual void RotateZ(int alpha)
        {
            modelMatrix.matrix[0, 0] = Math.Cos(alpha * Math.PI / 180);
            modelMatrix.matrix[0, 1] = -Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[1, 0] = Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[1, 1] = Math.Cos(alpha * Math.PI / 180);
        }

        public virtual void RotateY(int alpha)
        {
            modelMatrix.matrix[0, 0] = Math.Cos(alpha * Math.PI / 180);
            modelMatrix.matrix[0, 2] = Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[2, 0] = -Math.Sin(alpha * Math.PI / 180);
            modelMatrix.matrix[2, 2] = Math.Cos(alpha * Math.PI / 180);
        }

        public virtual void Translate(Vector3 offset)
        {
            Position = new Vector3(Position.X + offset.X, Position.Y + offset.Y, Position.Z + offset.Z);
            modelMatrix.matrix[0, 3] += offset.X;
            modelMatrix.matrix[1, 3] += offset.Y;
            modelMatrix.matrix[2, 3] += offset.Z;
        }

        public virtual void Draw(DirectBitmap canvas, Matrix projMatrix, Matrix viewMatrix, ShadingModels shadingModel)
        {
            Vector3[] pointsToDraw = new Vector3[points.Count];
            Vector3[] pointsModel = new Vector3[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                Vector4 point = new Vector4(points[i].X, points[i].Y, points[i].Z, 1);
                point = modelMatrix * point;
                pointsModel[i].X = point.X;
                pointsModel[i].Y = point.Y;
                pointsModel[i].Z = point.Z;
                point = viewMatrix * point;
                point = projMatrix * point;
                pointsToDraw[i].X = point.X + 700;
                pointsToDraw[i].Y = point.Y + 400;
                pointsToDraw[i].Z = point.Z;
            }

            this.normalMatrix = modelMatrix.Inverse().Transpose();

            Vector3 cameraPosition = new Vector3((float)viewMatrix.matrix[0, 3], (float)viewMatrix.matrix[1, 3], (float)viewMatrix.matrix[2, 3]);

            foreach (var triangle in triangles) 
            {
                var ba = pointsToDraw[triangle[0]] - pointsToDraw[triangle[1]];
                var ca = pointsToDraw[triangle[0]] - pointsToDraw[triangle[2]];
                if (Vector3.Cross(ba, ca).Z < 0 && colorGenerator != null)
                {
                    var index = triangles.IndexOf(triangle);
                    Vector4 normalVector4 = new Vector4(trianglesNormals[index].X, trianglesNormals[index].Y, trianglesNormals[index].Z, 0);
                    normalVector4 = normalMatrix * normalVector4;
                    if (shadingModel == ShadingModels.Constant)
                    {
                        var aColor = colorGenerator.GetColorVector3(pointsModel[triangle[0]], Vector3.Normalize(pointsModel[triangle[0]] - this.Position), cameraPosition);
                        var bColor = colorGenerator.GetColorVector3(pointsModel[triangle[1]], Vector3.Normalize(pointsModel[triangle[1]] - this.Position), cameraPosition);
                        var cColor = colorGenerator.GetColorVector3(pointsModel[triangle[2]], Vector3.Normalize(pointsModel[triangle[2]] - this.Position), cameraPosition);
                        var genColor = (aColor + bColor + cColor) / 3;
                        FillTriangle(canvas, pointsToDraw[triangle[0]], pointsToDraw[triangle[1]], pointsToDraw[triangle[2]], Color.FromArgb((int)genColor.X, (int)genColor.Y, (int)genColor.Z));
                    }
                    else if(shadingModel == ShadingModels.Gouraud)
                    {
                        FillTriangleGouraud(canvas, pointsToDraw[triangle[0]], pointsToDraw[triangle[1]], pointsToDraw[triangle[2]], pointsModel[triangle[0]],
                        pointsModel[triangle[1]], pointsModel[triangle[2]], new Vector3(normalVector4.X, normalVector4.Y, normalVector4.Z), cameraPosition);// Vector3.Normalize(new Vector3(color.R, color.G, color.B))
                    }
                    else if(shadingModel == ShadingModels.Phong)
                    {
                        FillTrianglePhong(canvas, pointsToDraw[triangle[0]], pointsToDraw[triangle[1]], pointsToDraw[triangle[2]], pointsModel[triangle[0]],
                        pointsModel[triangle[1]], pointsModel[triangle[2]], cameraPosition);
                    }
                }
            }
        }

        protected void FillTriangle(DirectBitmap canvas, Vector3 a, Vector3 b, Vector3 c, Color color)
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
                        float z = a.Z - (p.X * (x - a.X) + p.Y * (y - a.Y)) / p.Z;
                        if (x < canvas.Width && x > 0 && y < canvas.Height && y > 0 && z <= zBuffer[x, y])
                        {
                            zBuffer[x, y] = z;
                            canvas.SetPixel(x, y, color);
                        }
                    }
            }

        }

        protected void FillTriangleGouraud(DirectBitmap canvas, Vector3 a, Vector3 b, Vector3 c, Vector3 aModel, Vector3 bModel, Vector3 cModel, Vector3 normal, Vector3 cameraPosition)
        {
            var aColor = colorGenerator.GetColorVector3(aModel, new Vector3(normal.X, normal.Y, normal.Z), cameraPosition);
            var bColor = colorGenerator.GetColorVector3(bModel, new Vector3(normal.X, normal.Y, normal.Z), cameraPosition);
            var cColor = colorGenerator.GetColorVector3(cModel, new Vector3(normal.X, normal.Y, normal.Z), cameraPosition);
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
                        float z = a.Z - (p.X * (x - a.X) + p.Y * (y - a.Y)) / p.Z;
                        if (x < canvas.Width && x > 0 && y < canvas.Height && y > 0 && z <= zBuffer[x, y])
                        {
                            zBuffer[x, y] = z;
                            (float alpha, float beta, float gamma) = BarycentricCoords(trianglePoints.ToArray(), new Point(x, y));
                            Vector3 colorVector = alpha * aColor + beta * bColor + gamma * cColor;
                            colorVector = CutColorVector(colorVector);
                            var calcColor = Color.FromArgb((int)Math.Round(colorVector.X), (int)Math.Round(colorVector.Y), (int)Math.Round(colorVector.Z));
                            canvas.SetPixel(x, y, calcColor);
                        }
                    }
            }
        }

        protected void FillTrianglePhong(DirectBitmap canvas, Vector3 a, Vector3 b, Vector3 c, Vector3 aModel, Vector3 bModel, Vector3 cModel, Vector3 cameraPosition)
        {
            var aNormal = Vector3.Normalize(aModel - this.Position);
            var bNormal = Vector3.Normalize(bModel - this.Position);
            var cNormal = Vector3.Normalize(cModel - this.Position);
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
                        float z = a.Z - (p.X * (x - a.X) + p.Y * (y - a.Y)) / p.Z;
                        if (x < canvas.Width && x > 0 && y < canvas.Height && y > 0 && z <= zBuffer[x, y])
                        {
                            zBuffer[x, y] = z;
                            (float alpha, float beta, float gamma) = BarycentricCoords(trianglePoints.ToArray(), new Point(x, y));
                            Vector3 pointNormal = alpha * aNormal + beta * bNormal + gamma * cNormal;
                            Vector3 coords = alpha * aModel + beta * bModel + gamma * cModel;
                            var genColor = colorGenerator.GetColor(coords, pointNormal, cameraPosition);
                            canvas.SetPixel(x, y, genColor);
                        }
                    }
            }
        }

        protected (float, float, float) BarycentricCoords(Point[] triangle, Point point)
        {
            float alpha = (float)((triangle[1].Y - triangle[2].Y) * (point.X - triangle[2].X) + (triangle[2].X - triangle[1].X) * (point.Y - triangle[2].Y)) /
                (float)((triangle[1].Y - triangle[2].Y) * (triangle[0].X - triangle[2].X) + (triangle[2].X - triangle[1].X) * (triangle[0].Y - triangle[2].Y));
            float beta = (float)((triangle[2].Y - triangle[0].Y) * (point.X - triangle[2].X) + (triangle[0].X - triangle[2].X) * (point.Y - triangle[2].Y)) /
                (float)((triangle[1].Y - triangle[2].Y) * (triangle[0].X - triangle[2].X) + (triangle[2].X - triangle[1].X) * (triangle[0].Y - triangle[2].Y));
            float gamma = 1 - alpha - beta;
            return (alpha, beta, gamma);
        }

        protected Vector3 CutColorVector(Vector3 input)
        {
            Vector3 output = input;
            if (output.X < 0) output.X = 0;
            else if (output.X > 255) output.X = 255;
            if (output.Y < 0) output.Y = 0;
            else if (output.Y > 255) output.Y = 255;
            if (output.Z < 0) output.Z = 0;
            else if (output.Z > 255) output.Z = 255;
            return output;
        }
    }
}
