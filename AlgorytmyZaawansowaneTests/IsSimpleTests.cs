using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorytmyZaawansowane;
using System.Windows;
using System.Collections.Generic;

namespace AlgorytmyZaawansowaneTests
{
    [TestClass]
    public class IsSimpleTests
    {
        [TestMethod]
        public void IsSimple_Krawedz_Przecina_Krawedz()
        {
            Polygon poli = new Polygon(new List<Point>
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 0),
                new Point(1, 1),
            });

            bool isSimple = poli.IsSimple();

            Assert.IsFalse(isSimple);
        }
        [TestMethod]
        public void IsSimple_Krawedz_Pokrywa_Niesasiednia_Krawedz()
        {
            Polygon poli = new Polygon(new List<Point>
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(1, 1),
                new Point(2, 0),
            });

            bool isSimple = poli.IsSimple();

            Assert.IsFalse(isSimple);

        }
        [TestMethod]
        public void IsSimple_Krawedz_Pokrywa_Sasiednia_Krawedz()
        {
            Polygon poli = new Polygon(new List<Point>
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(1, 1),
                new Point(0, 1),
                new Point(1, 1),
            });

            bool isSimple = poli.IsSimple();

            Assert.IsFalse(isSimple);

        }
        [TestMethod]
        public void IsSimple_Krawedz_Przecina_Wierzcholek()
        {
            Polygon poli = new Polygon(new List<Point>
            {
                new Point(0, 0),
                new Point(2, 0),
                new Point(2, 1),
                new Point(1, 0),
                new Point(0, 1),
            });

            bool isSimple = poli.IsSimple();

            Assert.IsFalse(isSimple);

        }
        [TestMethod]
        public void IsSimple_Wierzcholek_Pokrywa_Wierzcholek()
        {
            Polygon poli = new Polygon(new List<Point>
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(0.5, 0.5),
                new Point(1, 1),
                new Point(0.5, 0.5),
            });

            bool isSimple = poli.IsSimple();

            Assert.IsFalse(isSimple);
        }

        [TestMethod]
        public void IsSimple_JestProsty()
        {
            Polygon poli = new Polygon(new List<Point>
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(0.15, 0.57),
                new Point(1, 1),
                new Point(1, 5),
            });

            bool isSimple = poli.IsSimple();

            Assert.IsTrue(isSimple);
        }
    }
}
