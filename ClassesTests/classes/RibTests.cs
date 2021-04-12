using Lab1Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesTests
{
    [TestClass()]
    public class RibTests
    {
        [TestMethod()]
        public void RibToStringTest()
        {
            Vertex start = new Vertex(67);
            Vertex end = new Vertex(98);
            Rib rib = new Rib((start, end), 800);

            string expectedStr = "67-98";

            Assert.AreEqual(expectedStr, rib.ToString());
        }

        [TestMethod()]
        public void InitRibTest()
        {
            Vertex start = new Vertex(67);
            Vertex end = new Vertex(98);
            Rib rib = new Rib((start, end), 800);

            int expectedValue = 800;
            int expectedStartName = 67;
            int expectedEndName = 98;

            Assert.AreEqual(expectedValue, rib.Value);
            Assert.AreEqual(expectedEndName, rib.End.Name);
            Assert.AreEqual(expectedStartName, rib.Start.Name);
        }


        [TestMethod()]
        public void RibEqualsTest()
        {
            Vertex x = new Vertex(11);
            Vertex y = new Vertex(12);

            Rib xy1 = new Rib((x, y), 10);
            Rib xy2 = new Rib((x, y), 10);
            Assert.IsTrue(Equals(xy2, xy1));

            Rib yx1 = new Rib((y, x), 10);
            Assert.IsFalse(Equals(yx1, xy1));


            Rib yx2 = new Rib((y, x), 12);
            Assert.IsFalse(Equals(yx2, yx1));


            Assert.IsFalse(Equals(yx2, xy1));
           
           
        }
    }
}