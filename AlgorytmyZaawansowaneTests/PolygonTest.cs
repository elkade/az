using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorytmyZaawansowane;
using System.Collections.Generic;

namespace AlgorytmyZaawansowaneTests
{
    [TestClass]
    public class PolygonTest
    {
        Point point = new Point(0, 0);

        [TestMethod]
        public void TestPointInside()
        {
            /// TEST POLYGON:
            // +-------+
            // |       |
            //-|---O---|------ ray
            // |       |
            // +-------+

            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(1, 1));
            pointList.Add(new Point(1, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsTrue(polygon.IsPointInside(point), "Point should be inside polygon");
        }

        [TestMethod]
        public void TestPointOutsideRight()
        {
            /// TEST POLYGON:
            //       +-------+
            //       |       |
            // --O---|-------|------ ray
            //       |       |
            //       +-------+

            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(1, 1));
            pointList.Add(new Point(2, 1));
            pointList.Add(new Point(2, -1));
            pointList.Add(new Point(1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsFalse(polygon.IsPointInside(point), "Point should be outside polygon");
        }

        [TestMethod]
        public void TestPointOutsideLeft()
        {
            /// TEST POLYGON:
            //       +-------+
            //       |       |
            // ------|-------|---O-- ray
            //       |       |
            //       +-------+

            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(-2, 1));
            pointList.Add(new Point(-2, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsFalse(polygon.IsPointInside(point), "Point should be outside polygon");
        }

        [TestMethod]
        public void TestPointVertexOut()
        {
            /// TEST POLYGON:
            // +------+
            // |       \
            //-|---O----+------ ray
            // |       /
            // +------+

            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(1, 1));
            pointList.Add(new Point(2, 0));
            pointList.Add(new Point(1, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon (pointList);

            Assert.IsTrue(polygon.IsPointInside(point), "Point should be inside polygon");
        }

        [TestMethod]
        public void TestPointVertexInAbove()
        {
            /// TEST POLYGON:
            // +------+   + 
            // |       \ /|
            //-|---O----+------ ray
            // |          |
            // +----------+

            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(1, 1));
            pointList.Add(new Point(2, 0));
            pointList.Add(new Point(3, 1));
            pointList.Add(new Point(3, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsTrue(polygon.IsPointInside(point), "Point should be inside polygon");
        }

        [TestMethod]
        public void TestPointVertexInBelow()
        {
            /// TEST POLYGON:
            // +----------+ 
            // |          |
            //-|---O----+-|---- ray
            // |       / \|
            // +------+   +

            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(3, 1));
            pointList.Add(new Point(3, -1));
            pointList.Add(new Point(2, 0));
            pointList.Add(new Point(1, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsTrue(polygon.IsPointInside(point), "Point should be inside polygon");
        }

        [TestMethod]
        public void TestPointSectionInAbove()
        {
            /// TEST POLYGON:
            // +------+       + 
            // |       \     /|
            //-|---O----+–––+-|---- ray
            // |              |
            // +--------------+
            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(1, 1));
            pointList.Add(new Point(2, 0));
            pointList.Add(new Point(3, 0));
            pointList.Add(new Point(4, 1));
            pointList.Add(new Point(4, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsTrue(polygon.IsPointInside(point), "Point should be inside polygon");
        }

        [TestMethod]
        public void TestPointSectionInAbove2()
        {
            /// TEST POLYGON:
            // +------+       + 
            // |       \     /|
            //-|---O----+–+–+-|---- ray
            // |              |
            // +--------------+
            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(1, 1));
            pointList.Add(new Point(2, 0));
            pointList.Add(new Point(2.5, 0));
            pointList.Add(new Point(3, 0));
            pointList.Add(new Point(4, 1));
            pointList.Add(new Point(4, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsTrue(polygon.IsPointInside(point), "Point should be inside polygon");
        }

        [TestMethod]
        public void TestPointSectionInBelow()
        {
            /// TEST POLYGON:
            // +--------------+ 
            // |              |
            //-|---O----+–––+-|---- ray
            // |       /     \|
            // +------+-------+
            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(4, 1));
            pointList.Add(new Point(4, -1));
            pointList.Add(new Point(3, 0));
            pointList.Add(new Point(2, 0));
            pointList.Add(new Point(1, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsTrue(polygon.IsPointInside(point), "Point should be inside polygon");
        }

        [TestMethod]
        public void TestPointSectionInBelow2()
        {
            /// TEST POLYGON:
            // +--------------+ 
            // |              |
            //-|---O----+–+–+-|---- ray
            // |       /     \|
            // +------+-------+
            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(4, 1));
            pointList.Add(new Point(4, -1));
            pointList.Add(new Point(3, 0));
            pointList.Add(new Point(2.5, 0));
            pointList.Add(new Point(2, 0));
            pointList.Add(new Point(1, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsTrue(polygon.IsPointInside(point), "Point should be inside polygon");
        }

        [TestMethod]
        public void TestPointSectionOut()
        {
            /// TEST POLYGON:
            // +------+
            // |       \       
            //-|---O----+–––+------ ray
            // |             \
            // +--------------+
            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(1, 1));
            pointList.Add(new Point(2, 0));
            pointList.Add(new Point(3, 0));
            pointList.Add(new Point(4, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsTrue(polygon.IsPointInside(point), "Point should be inside polygon");
        }

        [TestMethod]
        public void TestPointSectionOut2()
        {
            /// TEST POLYGON:
            // +------+
            // |       \       
            //-|---O----+–+–+------ ray
            // |             \
            // +--------------+
            IList<Point> pointList = new List<Point>();
            pointList.Add(new Point(-1, 1));
            pointList.Add(new Point(1, 1));
            pointList.Add(new Point(2, 0));
            pointList.Add(new Point(2.5, 0));
            pointList.Add(new Point(3, 0));
            pointList.Add(new Point(4, -1));
            pointList.Add(new Point(-1, -1));

            Polygon polygon = new Polygon(pointList);

            Assert.IsTrue(polygon.IsPointInside(point), "Point should be inside polygon");
        }
    }
}
