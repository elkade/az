using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AlgorytmyZaawansowane
{
    public class EdgeEventQueue
    {
        private EdgeEvent[] events;

        private int index = 0;

        public EdgeEvent NextEvent()
        {
            if (index < events.Count())
            {
                return events[index++];
            }
            return null;
        }

        public EdgeEventQueue(Polygon polygon)
        {
            events = new EdgeEvent[polygon.Count * 2];

            int arrayIndex = 0;
            Point? lastVertex = null;
            foreach (Point vertex in polygon.Vertices)
            {
                if (lastVertex == null)
                {
                    lastVertex = vertex;
                    continue;
                }
                EdgeEvent left, right;

                if (IsAfter((Point)lastVertex, vertex) > 0)
                {
                    left = new EdgeEvent(vertex, Side.LEFT, arrayIndex);
                    right = new EdgeEvent((Point)lastVertex, Side.RIGHT, arrayIndex);
                }
                else
                {
                    left = new EdgeEvent((Point)lastVertex, Side.LEFT, arrayIndex);
                    right = new EdgeEvent(vertex, Side.RIGHT, arrayIndex);
                }
                left.OtherEnd = right;
                right.OtherEnd = left;

                events[2 * arrayIndex] = left;
                events[2 * arrayIndex + 1] = right;

                lastVertex = vertex;
                arrayIndex++;
            }

            Array.Sort(events, delegate (EdgeEvent e1, EdgeEvent e2)
            {
                int after = IsAfter(e1.Point, e2.Point);
                if(after != 0)
                {
                    return after;
                }
                // ten sam punkt - sprawdzenie koniec/początek
                if (e1.Side == e2.Side)
                {
                    // nie powinno tak być raczej
                    return 0;
                }
                if (e1.Side == Side.LEFT)
                {
                    return 1;
                }
                return -1;
            });
        }

        private int IsAfter(Point p1, Point p2)
        {
            // test X
            if (p1.X > p2.X) return 1;
            if (p1.X < p2.X) return -1;
            // then test Y
            if (p1.Y > p2.Y) return 1;
            if (p1.Y < p2.Y) return -1;
            // this is the same point
            return 0;
        }
    }
}
