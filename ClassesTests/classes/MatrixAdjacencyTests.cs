using Lab1Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesTests
{
    [TestClass()]
    public class MatrixAdjacencyTests
    {
        [TestMethod()]
        public void DimTest()
        {

            MatrixAdjacency matrixAdjacency = new MatrixAdjacency(new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });

            int expectedDim = 3;

            Assert.AreEqual(expectedDim, matrixAdjacency.Dim());
        }

        [TestMethod()]
        public void InitMatrixAdjacencyTest()
        {
            MatrixAdjacency matrixAdjacency = new MatrixAdjacency(new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });

            int[,] expectedMatrix = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            CollectionAssert.AreEqual(expectedMatrix, matrixAdjacency.Data);
        }

        [TestMethod()]
        public void MatrixAdjacencyIndexTest()
        {
            MatrixAdjacency matrixAdjacency = new MatrixAdjacency(new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            int row = 1;
            int column = 2;

            int expectedValue = 6;

            Assert.AreEqual(expectedValue, matrixAdjacency[row, column]);

        }
    }
}