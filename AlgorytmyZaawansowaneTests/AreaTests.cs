using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorytmyZaawansowane;
using System.Windows;
using System.Collections.Generic;

namespace AlgorytmyZaawansowaneTests
{
    [TestClass]
    public class AreaTests
    {
        [TestMethod]
        public void Area1()
        {
            Polygon poli = new Polygon(new List<Point> { new Point(0, 0), new Point(0, 1), new Point(1, 1) });
            double area = poli.GetArea();

            Assert.AreEqual(0.5, area);
        }
        [TestMethod]
        public void Area2()
        {
            Polygon poli = new Polygon(new List<Point> { new Point(0, 0), new Point(0, -1), new Point(-1, -1) });
            double area = poli.GetArea();

            Assert.AreEqual(0.5, area);
        }
        [TestMethod]
        public void Area3()
        {
            Polygon poli = new Polygon(new List<Point> { new Point(-1, -1), new Point(0, -1), new Point(0, 0) });
            double area = poli.GetArea();

            Assert.AreEqual(0.5, area);
        }
        [TestMethod]
        public void Area4()
        {
            Polygon poli = new Polygon(new List<Point> { new Point(-0.5, -0.5), new Point(0.5, -0.5), new Point(0.5, 0.5) });
            double area = poli.GetArea();

            Assert.AreEqual(0.5, area);
        }
        [TestMethod]
        public void Area5()
        {
            Polygon poli = new Polygon(new List<Point>
            {
                new Point(0, 0),
                new Point(5, -1),
                new Point(5, 0),
                new Point(5, 1),
                new Point(4, 0),
                new Point(4, 1),
                new Point(3, 0),
                new Point(3, 1),
                new Point(2, 0),
                new Point(2, 1),
            });
            double area = poli.GetArea();

            Assert.AreEqual(5, area);
        }
    }
}
