using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;

namespace Worker.Parts
{
    public static class MatrixExtention
    {
        public static RepeatedField<string> ConvertMatrixToReapetedStr(this int[,] data)
        {

            RepeatedField<string> result = new RepeatedField<string>();

            for (int i = 0; i < data.GetLength(0); i++)
            {
                string line = "";

                for (int j = 0; j < data.GetLength(0); j++)
                    line += data[i, j].ToString() + ';';

                result.Add(line);
            }

            return result;
        }



        public static RepeatedField<RepeatedField<int>> ConvertMatrixTorepeatedRows(this int[,] data)
        {
            RepeatedField<RepeatedField<int>> result = new RepeatedField<RepeatedField<int>>();

            for (int i = 0; i < data.GetLength(0); i++)
            {
                RepeatedField<int> line = new RepeatedField<int>();
                
                for (int j = 0; j < data.GetLength(0); j++)
                    line.Add(data[i,j]);
                
                result.Add(line);
            }

            return result;
        }
        
        public static int[,] ConvertRepeatedFieldStrToMatrix( this RepeatedField<string> data)
        {
            
            int[,] matrix = new int[data.Count, data.Count];
            
            int i = 0;

            foreach (var line in data)
            {
                for (int j = 0; j < data.Count; j++)
                    matrix[i, j] = Convert.ToInt32(line.Split(';')[j]);

                i++;
            }

            return matrix;
        }
    }

}