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
            throw new NotImplementedException();
        }
        public double GetArea()
        {
            var area = Math.Abs(this.Take(Count - 1)
               .Select((p, i) => (this[i + 1].X - p.X) * (this[i + 1].Y + p.Y))
               .Sum() + (this[0].X - this[Count-1].X) * (this[0].Y + this[Count - 1].Y)) / 2.0;
            return area;
        }
        public override string ToString()
        {
            return string.Join(", ", this) + " Area: " + GetArea();
        }
    }

}
