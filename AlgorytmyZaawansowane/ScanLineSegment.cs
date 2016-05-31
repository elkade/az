using System.Windows;

namespace AlgorytmyZaawansowane
{
    public class ScanLineSegment
    {
        public ScanLineSegment Above { get; set; }

        public ScanLineSegment Below { get; set; }

        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }
    }
}