using Lab1Classes;
using Lab1Parts;
using Lab1Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using NLog;
using System;


namespace lab1
{
    class Program
    {
        private static Logger lg = LogManager.GetCurrentClassLogger();
        private static async Task Main(string[] args)
        {
            
            try
            {
                Console.WriteLine("add path to file with data:");
                string path = Console.ReadLine();
                lg.Info("начал создавать граф");
                Graph graph = CreateG.CreateGraph(path);
                lg.Info("закончил создавать граф");

                Console.WriteLine("add path to file for writing tables:");
                path = Console.ReadLine();

                Task taskForWriteMA = new Task(() => WriteToFile.WritePathTableToCsvFile(graph.GetMatrixAdjacency().Data, "Matrix Adjacency", path));

                Task<int[,]> taskForPathTable = new Task<int[,]>(() => DekstraAlgoritm.AlgoritmPathTable(graph));

                Task taskForGetListPath = new Task(() =>
                {
                    Console.WriteLine("Do you want to know list path?");
                    string answer = Console.ReadLine();
                    if (answer == "Yes" || answer == "yes" || answer == "да" || answer == "Да")
                    {
                        Console.WriteLine("add path to file for saving:");
                        string path = Console.ReadLine();
                        DekstraAlgoritm.AlgoritmListPath(graph, path);
                    }
                    else
                        lg.Info("пользователь отказался искать путь между вершинами");
                });

                Task taskForWritePT = taskForPathTable.ContinueWith( (pathTable) => WriteToFile.WritePathTableToCsvFile(taskForPathTable.Result, "Path table", path));


                taskForWriteMA.Start();
                taskForPathTable.Start();
                taskForGetListPath.Start();
         
                Console.WriteLine("waiting...");
                await Task.WhenAll(taskForWriteMA, taskForPathTable, taskForWritePT, taskForGetListPath);
                Console.WriteLine("everithing is done");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                lg.Error(ex.Message);
            }
            
        }


    }
}
