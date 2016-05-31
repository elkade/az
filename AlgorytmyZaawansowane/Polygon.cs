using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace AlgorytmyZaawansowane
{
    public class Polygon : List<Point>
    {
        public Polygon()
        {

        }

        public Polygon(IEnumerable<Point> collection) : base(collection)
        {

        }

        public IEnumerable<Point> Vertices//dodaje pierwszy wierzchołek na koniec, żeby uzyskać ostatnią krawędź
        {
            get
            {
                foreach (var v in this)
                    yield return v;
                yield return this[0];
            }
        }

        public bool IsPointInside(Point point)
        {
            bool isInside = false;
            for (int i = 0, j = Count - 1; i < Count; j = i++)
            {
                // TODO: vertex on ray

                if (((this[i].Y > point.Y) != (this[j].Y > point.Y)) &&
                ( point.X < this[i].X + (this[j].X - this[i].X) * (this[i].Y - point.Y) / (this[i].Y - this[j].Y)) )
                {
                    isInside = !isInside;
                }
            }
            return isInside;
        }

        public bool IsSimple()
        {
            EdgeEventQueue queue = new EdgeEventQueue(this);
            ScanLine scanLine = new ScanLine();

            for(EdgeEvent edgeEvent = queue.NextEvent(); edgeEvent != null; edgeEvent = queue.NextEvent())
            {
                if(edgeEvent.Side == Side.LEFT)
                {
                    ScanLineSegment segment = scanLine.Add(edgeEvent);
                    ScanLineSegment above = segment.Above;
                    ScanLineSegment below = segment.Below;
                    if (above != null && DoIntersect(segment.StartPoint, segment.EndPoint, above.StartPoint, above.EndPoint))
                    {
                        return false;
                    }
                    if (below != null && DoIntersect(segment.StartPoint, segment.EndPoint, below.StartPoint, below.EndPoint))
                    {
                        return false;
                    }
                }
                else
                {
                    ScanLineSegment segment = scanLine.Find(edgeEvent);
                    if (segment.Above == null || segment.Below == null)
                    {
                        continue;
                    }
                    if(DoIntersect(segment.Above.StartPoint, segment.Above.EndPoint, segment.Below.StartPoint, segment.Below.EndPoint))
                    {
                        return false;
                    }
                    scanLine.Remove(edgeEvent);
                }
            }
            return true;
        }

        public double GetArea()
        {
            var vertices = Vertices.ToList();
            var area = Math.Abs(vertices.Take(vertices.Count - 1)
               .Select((p, i) => (vertices[i + 1].X - p.X) * (vertices[i + 1].Y + p.Y))
               .Sum() /*+ (this[0].X - this[Count-1].X) * (this[0].Y + this[Count - 1].Y)*/) / 2.0;
            return area;
        }

        public override string ToString()
        {
            return string.Join(", ", this) + " Area: " + GetArea() + (IsSimple()? " SIMPLE":" NOT SIMPLE");
        }

        // Given three colinear points p, q, r, the function checks if
        // point q lies on line segment 'pr'
        private bool OnSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }

        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        private int Orientation(Point p, Point q, Point r)
        {
            double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (val == 0)
                return 0;  // colinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        // The main function that returns true if line segment 'p1q1'
        // and 'p2q2' intersect.
        private bool DoIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            // Find the four orientations needed for general and
            // special cases
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases
        }

        private bool IsBetween(Point a, Point b, Point c)
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
