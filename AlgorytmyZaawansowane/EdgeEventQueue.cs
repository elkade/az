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
                if (IsAfter((Point)lastVertex, vertex) > 0)
                {
                    events[2 * arrayIndex] = new EdgeEvent(vertex, Side.LEFT);
                    events[2 * arrayIndex + 1] = new EdgeEvent((Point)lastVertex, Side.RIGHT);
                }
                else
                {
                    events[2 * arrayIndex] = new EdgeEvent((Point)lastVertex, Side.LEFT);
                    events[2 * arrayIndex + 1] = new EdgeEvent(vertex, Side.RIGHT);
                }
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
                if(e1.Side == e2.Side)
                {
                    return 0;
                }
                if (e1.Side == Side.RIGHT)
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
