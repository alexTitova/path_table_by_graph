using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using System.IO;
using Lab1Classes;
using NLog;

namespace Lab1Parts
{
    public class WriteToFile
    {
        private static Logger lg = LogManager.GetCurrentClassLogger();

        public static void WritePathTableToCsvFile(int[,] table, string message, string filePath)
        {
            lg.Info("начал записывать " + message);

            using (StreamWriter file = new StreamWriter(@filePath, true))
            {
                file.WriteLine(message); ;
                for (int i = 0; i < table.GetLength(0); i++)
                {
                    for (int j = 0; j < table.GetLength(0); j++)
                    {
                        file.Write(table[i, j].ToString() + ";");
                    }

                    file.WriteLine();
                }


                file.WriteLine();
                file.Close();
            }

            lg.Info("закончил записывать " + message);
        }


        public static void WritePathVertexesToCsvFile(List<Vertex> vertexes, string filePath)
        {
            using (StreamWriter file = new StreamWriter(@filePath, true))
            {
                file.WriteLine("List vertexes of path");
                foreach (Vertex vertex in vertexes)
                {
                    file.Write(vertex.ToString() + ";");
                }

                file.WriteLine();
                file.WriteLine();
                file.Close();
            }
        }

        public static void WritePathVertexesToCsvFile((int, int) path, string message, string filePath)
        {
            using (StreamWriter file = new StreamWriter(@filePath, true))
            {
                file.WriteLine(message);
                file.Write(path.ToString() + ";");

                file.WriteLine();
                file.WriteLine();
                file.Close();
            }
        }

    }
}
             

