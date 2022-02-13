using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Project4.ColorFilling
{
    public class ScanLine
    {
        private List<AETPointer> AET;
        private readonly List<Point> polygon;
        private List<AETPointer>[] ET;
        public ScanLine(List<Point> polygon)
        {
            this.AET = new List<AETPointer>();
            this.polygon = polygon;
            this.BucketSort();
        }
        public IEnumerable<(List<int> xList, int y)> GetIntersectionPoints()
        {
            int yMin = polygon.Select(p => p.Y).Min();
            int yMax = polygon.Select(p => p.Y).Max();
            for (int y = yMin; y < yMax; y++)
            {
                AET.AddRange(ET[y - yMin]);
                AET.RemoveAll(pointer => pointer.yMax <= y);
                yield return (AET.Select(pointer => (int)Math.Round(pointer.x)).OrderBy(x => x).ToList(), y);
                foreach (var pointer in AET)
                    pointer.UpdateX();
            }
        }

        private void BucketSort()
        {
            int yMin = polygon.Select(p => p.Y).Min();
            int number = polygon.Select(p => p.Y).Max() - yMin + 1;
            ET = new List<AETPointer>[number];
            for (int i = 0; i < number; i++)
                ET[i] = new List<AETPointer>();
            for (int i = 0; i < polygon.Count; i++)
            {
                int index, x, yMax;
                if (polygon[i].Y < polygon[(i - 1 + polygon.Count) % polygon.Count].Y)
                {
                    index = polygon[i].Y - yMin;
                    x = polygon[i].X;
                    yMax = polygon[(i - 1 + polygon.Count) % polygon.Count].Y;
                }
                else
                {
                    index = polygon[(i - 1 + polygon.Count) % polygon.Count].Y - yMin;
                    x = polygon[(i - 1 + polygon.Count) % polygon.Count].X;
                    yMax = polygon[i].Y;
                }
                ET[index].Add(new AETPointer(yMax, x, Geometry.Slope(polygon[i], polygon[(i - 1 + polygon.Count) % polygon.Count])));
            }
            foreach (var bucket in ET)
                bucket.Sort();
        }
    }
    public class AETPointer : IComparable<AETPointer>
    {
        public int yMax;
        public double x;
        public double _m;
        public AETPointer(int yMax, double x, double m)
        {
            this.yMax = yMax;
            this.x = x;
            if (m == Geometry.Infinity)
                _m = Geometry.Infinity;
            else if (m == 0)
                _m = 0;
            else _m = 1 / m;
        }

        public int CompareTo(AETPointer other)
        {
            var xCmp = x.CompareTo(other.x);
            return xCmp == 0 ? yMax.CompareTo(other.yMax) : xCmp;
        }

        public void UpdateX()
        {
            if (_m != Geometry.Infinity)
                x += _m;
        }
    }
}
