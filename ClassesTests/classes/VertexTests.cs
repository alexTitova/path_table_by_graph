using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesTests
{
    [TestClass()]
    public class VertexTests
    {
        [TestMethod()]
        public void VertexToStringTest()
        {
            Vertex vertex = new Vertex(123);

            string expectedStr = "123";

            Assert.AreEqual(expectedStr, vertex.ToString());
        }


        [TestMethod()]
        public void InitVertexWithNameTest()
        {
            Vertex vertex = new Vertex(5467);

            int expectedName = 5467;
            int expectedMark = 1000000;

            Assert.AreEqual(expectedMark, vertex.Mark);
            Assert.AreEqual(expectedName, vertex.Name);
            Assert.IsFalse(vertex.IsChecked);
            Assert.IsNull(vertex.Dad);

        }


        [TestMethod()]
        public void InitVertexWithNameMarkTest()
        {
            Vertex vertex = new Vertex(54, 46);

            int expectedName = 54;
            int expectedMark = 46;

            Assert.AreEqual(expectedMark, vertex.Mark);
            Assert.AreEqual(expectedName, vertex.Name);
            Assert.IsFalse(vertex.IsChecked);
            Assert.IsNull(vertex.Dad);

        }


        [TestMethod()]
        public void VeretxEqualsTest()
        {
            Vertex x = new Vertex(11, 10);
            Vertex y = new Vertex(11, 10);
            Assert.IsTrue(Equals(x, y));

            Vertex a = new Vertex(11, 10);
            Vertex b = new Vertex(11, 9);
            Assert.IsFalse(Equals(a, b));

            Vertex c = new Vertex(1, 10);
            Vertex e = new Vertex(2, 10);
            Assert.IsFalse(Equals(c, e));
        }
    }
}