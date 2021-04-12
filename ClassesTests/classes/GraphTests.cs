using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesTests
{
    [TestClass()]
    public class GraphTests
    {
        private Graph graph;

        [TestInitialize]
        public void TestInitialize()
        {
            Vertex a = new Vertex(101);
            Vertex b = new Vertex(102);
            Vertex c = new Vertex(103);
            Vertex d = new Vertex(104);
            Vertex e = new Vertex(105);
            Vertex h = new Vertex(106);

            SortedSet<Vertex> vertexes = new SortedSet<Vertex>() { a, b, c, d, e, h };


            Rib ab = new Rib((a, b), 7);
            Rib ad = new Rib((a, d), 1);
            Rib ae = new Rib((a, e), 3);

            Rib bc = new Rib((b, c), 2);
            Rib be = new Rib((b, e), 37);

            Rib ca = new Rib((c, a), 20);
            Rib cd = new Rib((c, d), 1);

            Rib dc = new Rib((d, c), 1);
            Rib de = new Rib((d, e), 10);
            Rib db = new Rib((d, b), 5);

            Rib ea = new Rib((e, a), 3);
            Rib ec = new Rib((e, c), 1);

            List<Rib> ribs = new List<Rib>() { ab, ad, ae, bc, be, ca, cd, dc, de, db, ea, ec };

            graph = new Graph(vertexes, ribs);

        }


        [TestMethod()]
        public void InitGraphTest()
        {
            Vertex a = new Vertex(101);
            Vertex b = new Vertex(102);
            Vertex c = new Vertex(103);
            Vertex d = new Vertex(104);
            Vertex e = new Vertex(105);
            Vertex h = new Vertex(106);

            SortedSet<Vertex> expectedVertexes = new SortedSet<Vertex>() { a, b, c, d, e, h };


            Rib ab = new Rib((a, b), 7);
            Rib ad = new Rib((a, d), 1);
            Rib ae = new Rib((a, e), 3);

            Rib bc = new Rib((b, c), 2);
            Rib be = new Rib((b, e), 37);

            Rib ca = new Rib((c, a), 20);
            Rib cd = new Rib((c, d), 1);

            Rib dc = new Rib((d, c), 1);
            Rib de = new Rib((d, e), 10);
            Rib db = new Rib((d, b), 5);

            Rib ea = new Rib((e, a), 3);
            Rib ec = new Rib((e, c), 1);

            List<Rib> expexctedRibs = new List<Rib>() { ab, ad, ae, bc, be, ca, cd, dc, de, db, ea, ec };

            CollectionAssert.AreEqual(expectedVertexes, graph.Vertexes);
            CollectionAssert.AreEqual(expexctedRibs, graph.Ribs);

        }

        [TestMethod()]
        public void CountVertexesTest()
        {
            int expectedCountVertexes = 6;

            Assert.AreEqual(expectedCountVertexes, graph.CountVertexes());
        }

        [TestMethod()]
        public void CountRibsTest()
        {
            int expectedCountRibs = 12;

            Assert.AreEqual(expectedCountRibs, graph.CountRibs());
        }


        [TestMethod()]
        [ExpectedException(typeof(ExceptionAlreadyExist))]
        public void AddVertexTest()
        {
            

            Vertex a = new Vertex(101);
            Vertex b = new Vertex(102);
            Vertex c = new Vertex(103);
            Vertex d = new Vertex(104);
            Vertex e = new Vertex(105);
            Vertex h = new Vertex(106);
            Vertex extra = new Vertex(110);

            SortedSet<Vertex> expectedVertexes = new SortedSet<Vertex>() { a, b, c, d, e, h, };
            expectedVertexes.Add(extra);

            graph.AddVertex(extra);

            CollectionAssert.AreEqual(expectedVertexes, graph.Vertexes);

            Vertex extra2 = new Vertex(106);
            graph.AddVertex(extra2);
            // выброситмся исключение ExceptionAlreadyExist
        }


        [TestMethod()]
        public void AddRibTest()
        {
            Vertex a = new Vertex(101);
            Vertex b = new Vertex(102);
            Vertex c = new Vertex(103);
            Vertex d = new Vertex(104);
            Vertex e = new Vertex(105);
            Vertex h = new Vertex(106);

            Rib ab = new Rib((a, b), 7);
            Rib ad = new Rib((a, d), 1);
            Rib ae = new Rib((a, e), 3);

            Rib bc = new Rib((b, c), 2);
            Rib be = new Rib((b, e), 37);

            Rib ca = new Rib((c, a), 20);
            Rib cd = new Rib((c, d), 1);

            Rib dc = new Rib((d, c), 1);
            Rib de = new Rib((d, e), 10);
            Rib db = new Rib((d, b), 5);

            Rib ea = new Rib((e, a), 3);
            Rib ec = new Rib((e, c), 1);

            List<Rib> expectedRibs = new List<Rib>() { ab, ad, ae, bc, be, ca, cd, dc, de, db, ea, ec };

            Rib extra1 = new Rib((c, b), 10);
            expectedRibs.Add(extra1);
            graph.AddRib(extra1);
            CollectionAssert.AreEqual(expectedRibs, graph.Ribs);

            try
            {
                graph.AddRib(ec);
                Assert.Fail();
            }
            catch(ExceptionAlreadyExist ex)
            {
                // такое ребро уже есть
            }

            try
            {
                graph.AddRib(new Rib((a, new Vertex(111)), 10));
                Assert.Fail();
            }
            catch (ExceptionDoesNotExist ex)
            {
                // не существует одной из вершин
            }

        }


        [TestMethod()]
        public void GetRibTest()
        {
            Rib actualRib = graph.GetRib((new Vertex(101), new Vertex(102)));

            Rib expectedRib = new Rib((new Vertex(101), new Vertex(102)), 7);

            Assert.AreEqual(expectedRib, actualRib);

            try
            {
                Rib aclualRib1 = graph.GetRib((new Vertex(101), new Vertex(106)));
                Assert.Fail();
            }
            catch(ExceptionDoesNotExist ex)
            {
                //нет такого ребра
            }
        }

        [TestMethod()]
        public void GetOutGoingRibsTest()
        {
            Vertex a = new Vertex(101);
            Vertex b = new Vertex(102);
            Vertex d = new Vertex(104);
            Vertex e = new Vertex(105);

            Rib ab = new Rib((a, b), 7);
            Rib ad = new Rib((a, d), 1);
            Rib ae = new Rib((a, e), 3);

            List<Rib> expectedRibs = new List<Rib>() { ab, ad, ae };

            List<Rib> aclualRibs = graph.GetOutGoingRibs(a);

            CollectionAssert.AreEqual(expectedRibs, aclualRibs);
        }


        [TestMethod()]
        public void GetMatrixAdjacencyTest()
        {
            int[,] expectedMatrix = new int[6, 6]
                { {0,7,0,1,3,0},
                { 0,0,2,0,37,0},
                { 20,0,0,1,0,0},
                {0,5,1,0,10,0 },
                {3,0,1,2,0,0 },
                { 0,0,0,0,0,0} };

            int[,] aclualMatrix = graph.GetMatrixAdjacency().Data;

            Assert.AreEqual(expectedMatrix.ToString(), aclualMatrix.ToString());

        }

    }
}