using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AlgorytmyZaawansowane
{
    public class EdgeEvent
    {
        public Side Side { get; set; }

        public Point Point { get; set; }

        public EdgeEvent(Point point, Side side)
        {
            Point = new Point(point.X, point.Y);
            Side = side;
        }
    }

    public enum Side
    {
        LEFT,
        RIGHT
    }
}
