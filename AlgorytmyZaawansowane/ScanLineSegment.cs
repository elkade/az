using System.Windows;

namespace AlgorytmyZaawansowane
{
    public class ScanLineSegment
    {
        public int SegmentIndex { get; set; }

        public ScanLineSegment Above { get; set; }

        public ScanLineSegment Below { get; set; }

        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public int EdgeIndex { get; set; }

        public override bool Equals(object obj)
        {
            ScanLineSegment otherSegment = obj as ScanLineSegment;
            if (otherSegment == null)
                return false;

            return this.StartPoint.Equals(otherSegment.StartPoint) && this.EndPoint.Equals(otherSegment.EndPoint);
        }
    }
}