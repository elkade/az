using System;
using System.Collections.Generic;
using System.Windows;

namespace AlgorytmyZaawansowane
{
    public class ScanLine
    {
        SortedSet<ScanLineSegment> tree;

        private int index = 0;

        public ScanLine()
        {
            tree = new SortedSet<ScanLineSegment>(new SegmentComparer());
        }

        public ScanLineSegment Add(EdgeEvent edgeEvent)
        {
            if(edgeEvent.Side != Side.LEFT)
            {
                throw new System.Exception("Trying to add non-left event to tree!");
            }

            ScanLineSegment newSegment = new ScanLineSegment();
            newSegment.SegmentIndex = index++;

            newSegment.StartPoint = edgeEvent.Point;
            newSegment.EndPoint = edgeEvent.OtherEnd.Point;
            newSegment.EdgeIndex = edgeEvent.EdgeIndex;

            tree.Add(newSegment);

            newSegment.Below = Previous(newSegment);
            if (newSegment.Below != null)
                newSegment.Below.Above = newSegment;

            newSegment.Above = Next(newSegment);
            if (newSegment.Above != null)
                newSegment.Above.Below = newSegment;

            return newSegment;
        }
        
        public void Remove(EdgeEvent edgeEvent)
        {
            if (edgeEvent.Side != Side.RIGHT)
            {
                throw new System.Exception("Trying to remove at non-right event!");
            }

            ScanLineSegment segment = Find(edgeEvent);
            if(segment.Above != null)
            {
                segment.Above.Below = segment.Below;
            }
            if(segment.Below != null)
            {
                segment.Below.Above = segment.Above;
            }

            tree.Remove(segment);
        }

        public ScanLineSegment Find(EdgeEvent edgeEvent)
        {
            Point left = edgeEvent.Side == Side.LEFT ? edgeEvent.Point : edgeEvent.OtherEnd.Point;
            Point right = edgeEvent.Side == Side.LEFT ? edgeEvent.OtherEnd.Point : edgeEvent.Point;

            foreach (ScanLineSegment segmentInTree in tree)
            {
                if (segmentInTree.StartPoint.Equals(left) && segmentInTree.EndPoint.Equals(right))
                    return segmentInTree;
            }
            return null;
        }

        private ScanLineSegment Previous(ScanLineSegment segment)
        {
            ScanLineSegment previous = null;
            foreach(ScanLineSegment segmentInTree in tree)
            {
                if (segmentInTree != segment)
                {
                    previous = segmentInTree;
                }
                else
                {
                    break;
                }
            }
            return previous;
        }

        private ScanLineSegment Next(ScanLineSegment segment)
        {
            bool after = false;
            foreach (ScanLineSegment segmentInTree in tree)
            {
                if(after)
                {
                    return segmentInTree;
                }
                if (segmentInTree == segment)
                {
                    after = true;
                }
            }
            return null;
        }
    }

    public class SegmentComparer : IComparer<ScanLineSegment>
    {
        public int Compare(ScanLineSegment x, ScanLineSegment y)
        {
            if (x.StartPoint.Equals(y.StartPoint) && x.EndPoint.Equals(y.EndPoint))
            {
                // equal
                return 0;
            }

            ScanLineSegment first = x.SegmentIndex < y.SegmentIndex ? x : y;
            ScanLineSegment second = x.SegmentIndex < y.SegmentIndex ? y : x;

            double xCoord = second.StartPoint.X;

            double firstY = first.StartPoint.Y + (xCoord - first.StartPoint.X) * (first.EndPoint.Y - first.StartPoint.Y);

            if (firstY - second.StartPoint.Y < 0)
                return 1;
            if (firstY - second.StartPoint.Y > 0)
                return -1;

            if(first.StartPoint.Equals(second.StartPoint))
            {
                // nachylenie
                if ((first.EndPoint.Y - first.StartPoint.Y)/ (first.EndPoint.X - first.StartPoint.X + double.Epsilon) > (second.EndPoint.Y - second.StartPoint.Y) / (second.EndPoint.X - second.StartPoint.X))
                    return -1;
                else
                    return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
