﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AlgorytmyZaawansowane
{
    public class EdgeEvent
    {
        public int EdgeIndex { get; set; }

        public Side Side { get; set; }

        public Point Point { get; set; }

        public EdgeEvent OtherEnd { get; set; }

        public EdgeEvent(Point point, Side side, int edgeIndex)
        {
            Point = new Point(point.X, point.Y);
            Side = side;
            EdgeIndex = edgeIndex;
        }
    }

    public enum Side
    {
        LEFT,
        RIGHT
    }
}
