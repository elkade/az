using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace AlgorytmyZaawansowane
{
    class Polygon : List<Point>
    {
        public Polygon()
        {

        }

        private IEnumerable<Point> _vertices
        {
            get
            {
                foreach (var v in this)
                    yield return v;
                yield return this[0];
            }
        }

        //public bool IsPointInside(Point point)
        //{
        //    bool isInside = false;
        //    for (int i = 0, j = Count - 1; i < Count; j = i++)
        //    {
        //        if (((this[i].Y > point.Y) != (this[j].Y > point.Y)) &&
        //        (point.X < (Points[j].X - Points[i].X) * (point.Y - Points[i].Y) / (Points[j].Y - Points[i].Y) + Points[i].X))
        //        {
        //            isInside = !isInside;
        //        }
        //    }
        //    return isInside;
        //}
        public bool IsSimple()
        {
            if (Count < 3) throw new InvalidOperationException("Too few vertices.");
            var vertices = _vertices.ToList();
            for (int i = 1; i < vertices.Count; i++)
            {
                for (int j = i + 1; j < vertices.Count; j++)
                {
                    if (DoLinesIntersect(vertices[i - 1], vertices[i], vertices[j - 1], vertices[j])) {
                        if (j == i + 1)
                        {
                            if (IsBetween(vertices[i - 1], vertices[i], vertices[j]) || IsBetween(vertices[j - 1], vertices[j], vertices[i - 1]))
                                //dwa kolejne pokrywają się
                                return false;
                        }else if(i == 1 && j == vertices.Count - 1)
                        {
                            if (IsBetween(vertices[i - 1], vertices[i], vertices[j-1]) || IsBetween(vertices[j - 1], vertices[j], vertices[i]))
                                //pierwszy i ostatni pokrywają się
                                return false;
                        }

                        else
                            return false;
                    }
                }
            }
            return true;
        }
        public double GetArea()
        {
            var vertices = _vertices.ToList();
            var area = Math.Abs(vertices.Take(vertices.Count - 1)
               .Select((p, i) => (vertices[i + 1].X - p.X) * (vertices[i + 1].Y + p.Y))
               .Sum() /*+ (this[0].X - this[Count-1].X) * (this[0].Y + this[Count - 1].Y)*/) / 2.0;
            return area;
        }
        public override string ToString()
        {
            return string.Join(", ", this) + " Area: " + GetArea() + (IsSimple()? " SIMPLE":" NOT SIMPLE");
        }
        public static bool DoLinesIntersect(Point p1, Point p2, Point p3, Point p4)
        {
            return CrossProduct(p1, p2, p3) !=
                   CrossProduct(p1, p2, p4) ||
                   CrossProduct(p3, p4, p1) !=
                   CrossProduct(p3, p4, p2);
        }
        public static double CrossProduct(Point p1, Point p2, Point p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
        }
        public static bool IsBetween(Point a, Point b, Point c)
        {
            var crossproduct = (c.Y - a.Y) * (b.X - a.X) - (c.X - a.X) * (b.Y - a.Y);
            if (Math.Abs(crossproduct) > double.Epsilon) return false;

            var dotproduct = (c.X - a.X) * (b.X - a.X) + (c.Y - a.Y) * (b.Y - a.Y);
            if (dotproduct < 0) return false;

            var squaredlengthba = (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
            if (dotproduct > squaredlengthba) return false;

            return true;
        }
    }

}
