using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1Classes
{
    public class MatrixAdjacency
    {
        private int[,] data;

        public MatrixAdjacency() { }
        public MatrixAdjacency(int[,] data) 
        {
            this.Data = data;
        }

        public int[,] Data { get; }

        public int Dim ()
        {
            return this.Data.GetLength(0);
        }

        public int this[int row, int column]
        {
            get => this.Data[row, column];

            set => this.Data[row, column] = value;
        }

    }

}
