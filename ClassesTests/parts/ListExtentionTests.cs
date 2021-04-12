using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1Parts;
using Lab1Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartTests
{
    [TestClass()]
    public class ListExtentionTests
    {
        private List<Vertex> data;

        [TestInitialize]
        public void TestInitialize()
        {
            Vertex a = new Vertex(101, 45);
            Vertex b = new Vertex(102, 4);
            Vertex c = new Vertex(103, 69);
            Vertex d = new Vertex(104, 25);
            Vertex e = new Vertex(105, 1);
            Vertex h = new Vertex(106, 70);

            data = new List<Vertex>() { e, a, c, b, h, d };
        }

        [TestMethod()]
        public void FindMinMarkExtentionTest()
        {
            Vertex expectedVertex = new Vertex(105, 1);

            Vertex actualVertex = data.FindMinMark();

            Assert.AreEqual(expectedVertex, actualVertex);
        }


        [TestMethod()]
        public void FindIndexForNotSortedExtensionTest()
        {
            int expectedIndex = 4;

            int actualIndex = data.FindIndexForNotSorted(new Vertex(106));

            Assert.AreEqual(expectedIndex, actualIndex);
        }
    }
}