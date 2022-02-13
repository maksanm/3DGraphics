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
    public class SphereWithReflector : Sphere
    {
        private LightSource reflector;

        public SphereWithReflector(Vector3 position, int radius, Color color, double[,] zBuffer, LightSource reflector) : base(position, radius, color, zBuffer)
        {
            this.reflector = reflector;
        }

        public override void RotateX(int alpha)
        {
            Matrix matrix = new Matrix(3, 3);
            matrix.matrix = new double[,] { { 1, 0, 0 }, { 0, Math.Cos(alpha * Math.PI / 180), -Math.Sin(alpha * Math.PI / 180) }, { 0, Math.Sin(alpha * Math.PI / 180), Math.Cos(alpha * Math.PI / 180) } };
            reflector.direction = matrix * reflector.direction;
            base.RotateX(alpha);
        }

        public override void RotateY(int alpha)
        {
            Matrix matrix = new Matrix(3, 3);
            matrix.matrix = new double[,] { { Math.Cos(alpha * Math.PI / 180), 0, Math.Sin(alpha * Math.PI / 180) }, { 0, 1, 0 }, { -Math.Sin(alpha * Math.PI / 180), 0, Math.Cos(alpha * Math.PI / 180) } };
            reflector.direction = matrix * reflector.direction;
            base.RotateY(alpha);
        }

        public override void RotateZ(int alpha)
        {
            Matrix matrix = new Matrix(3, 3);
            matrix.matrix = new double[,] { { Math.Cos(alpha * Math.PI / 180), -Math.Sin(alpha * Math.PI / 180), 0 }, { Math.Sin(alpha * Math.PI / 180), Math.Cos(alpha * Math.PI / 180), 0 }, { 0, 0, 1 } };
            reflector.direction = matrix * reflector.direction;
            base.RotateZ(alpha);
        }

        public override void Translate(Vector3 offset)
        {
            reflector.position = reflector.position + offset;
            base.Translate(offset);
        }
    }
}
