using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Project4.ColorFilling
{
    public static class Geometry
    {
        public const double Eps = 1e-8;
        public const double Infinity = 1 / Eps;
        public static double Slope(Point p1, Point p2)
        {
            double m = Math.Abs(p2.X - p1.X) < Eps ? Infinity : (double)(p2.Y - p1.Y) / (double)(p2.X - p1.X);
            return m;
        }
    }
}
