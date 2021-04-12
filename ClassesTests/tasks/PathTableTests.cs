using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1Tasks;
using Lab1Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PathTableTests
{
    [TestClass()]
    public class PathTableTests
    {
        [TestMethod()]
        public void AlgoritmPathTableTest()
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

            Graph graph = new Graph(vertexes, ribs);

            int[,] expectedPathTable = new int[6, 6]
            { {0, 6,2,1,3,0 },
            { 16,0,2,3,14,0},
            { 14,6,0,1,11,0},
            { 13,5,1,0,10,0},
            { 3,7,1,2,0,0},
            { 0,0,0,0,0,0}};


            int[,] actualPathTable = DekstraAlgoritm.AlgoritmPathTable(graph);

            Assert.AreEqual(expectedPathTable.ToString(), actualPathTable.ToString());


        }
    }
}