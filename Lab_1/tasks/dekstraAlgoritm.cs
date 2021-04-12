using Lab1Classes;
using Lab1Parts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;
using System.Threading.Tasks;
using NLog;

namespace Lab1Tasks
{
    public class DekstraAlgoritm
    {

        private static Logger lg = LogManager.GetCurrentClassLogger();



        // функция где будет распарлеливаться вычисления и собераться вместе 
        // возврашать должна таблицу расстояний+последовательность вершин рути между указанными то  есть пару (int[,], string)
        public static int[,] AlgoritmPathTable(Graph graph)
        {
            int[,] pathTable = new int[graph.CountVertexes(), graph.CountVertexes()];

            lg.Info("начал считать таблицу расстояний");
            Parallel.ForEach(graph.Vertexes, start => ForLineDekstra(graph, start, pathTable));
            lg.Info("закончил считать таблицу расстояний");

            return pathTable;
        }


        //активное взаимодейсивте с пользователем для поиска последовательности пути
        public static void AlgoritmListPath(Graph graph, string filePath)
        {
            string answer = "yes";

            while (answer == "Yes" || answer == "yes" || answer == "да" || answer == "Да")
            {
                List<Vertex> pathVertexes = new List<Vertex>();
                Console.WriteLine("Write names of vertex:");
                int start = Convert.ToInt32(Console.ReadLine());
                int end = Convert.ToInt32(Console.ReadLine());
                Vertex startV = new Vertex(start);
                Vertex endV = new Vertex(end);

                if (graph.Vertexes.Contains(startV) && graph.Vertexes.Contains(endV))
                {
                    lg.Debug("начал считать путь " + start + " - " + end);

                    try
                    {
                        ForSearchPath(graph, (startV, endV), pathVertexes,filePath);
                    }
                    catch (ExceptionDoesNotExist ex)
                    {
                        WriteToFile.WritePathVertexesToCsvFile((start, end), ex.Message, filePath);
                        lg.Warn("отсутствует путь " + start + " - " + end);
                    }

                }
                else
                {
                    lg.Warn("отсутствует вершина " + start + " или " + end);
                    WriteToFile.WritePathVertexesToCsvFile((start, end), "One of vertexes does not exist", filePath);
                }


                Console.WriteLine("Do you want to know list path?");
                answer = Console.ReadLine();
            }


        }



        //вычисляется и собирается строка таблицы пути
        private static void ForLineDekstra(Graph graph, Vertex start, int[,] pathTable)
        {
            lg.Debug("начал счиать таблицу расстояний для вершины " + start);
            List<Vertex> tmpVertexes = new List<Vertex>();

            //инициализация списка вершин, чтобы не испортить список вершин другим потокам, нулевой элемент это начальная вершина.
            tmpVertexes.Add(new Vertex(start.Name, 0));
            foreach (Vertex unit in graph.Vertexes)
            {
                // проверка чтобы начальная вершина была одна
                if (unit.Name != start.Name)
                    tmpVertexes.Add(new Vertex(unit.Name));
            }

            //считает путь от начальной вершины до всех остальных
            OneStep(tmpVertexes, graph);

            //записывается строка в таблицу
            int line = graph.Vertexes.ToList().BinarySearch(start);
            GetPathTable(tmpVertexes, pathTable, line);
            lg.Debug("закончил считать таблицу расстояний для вершины " + start);
        }

        private static void ForSearchPath(Graph graph, (Vertex, Vertex) path, List<Vertex> pathVertexes, string filePath)
        {
            GetVertexesPath(graph, path, pathVertexes);
            lg.Debug("закончил считать путь " + path.Item1 + " - " + path.Item2);

            // добавить ассинхронную запись
            lg.Debug("начал записывать путь " + path.Item1 + " - " + path.Item2);
            WriteToFile.WritePathVertexesToCsvFile(pathVertexes, filePath);
            lg.Debug("закончил записывать путь " + path.Item1 + " - " + path.Item2);
        }

        // проходит как раз этот алгоритм
        // считает новые метки вершин
        private static void OneStep(List<Vertex> vertexes, Graph graph)
        {
            List<Rib> outgoingRibs = new List<Rib>();
            Vertex curretMinMarkVertex = new Vertex();

            for (int ww = 0; ww < vertexes.Count(); ww++)
            {
                // вычисление непроверенной вершины с минимальной оценкой
                curretMinMarkVertex = vertexes.FindMinMark();

                //поиск исходящих ребер
                outgoingRibs = graph.GetOutGoingRibs(curretMinMarkVertex);

                foreach (Rib rib in outgoingRibs)
                {
                    int i = vertexes.FindIndexForNotSorted(rib.End);

                    ChangeMark(vertexes[i], curretMinMarkVertex, rib.Value);
                }

                curretMinMarkVertex.IsChecked = true;
            }
        }


        private static void GetVertexesPath(Graph graph, (Vertex, Vertex) path, List<Vertex> pathVertexes)
        {
            List<Vertex> tmpVertexes = new List<Vertex>();

            //инициализация списка вершин, чтобы не испортить список вершин другим потокам, нулевой элемент это начальная вершина.
            tmpVertexes.Add(new Vertex(path.Item1.Name, 0));
            foreach (Vertex unit in graph.Vertexes)
            {
                // проверка чтобы начальная вершина была одна
                if (unit.Name != path.Item1.Name)
                    tmpVertexes.Add(new Vertex(unit.Name));
            }

            //считает путь от начальной вершины до всех остальных
            OneStep(tmpVertexes, graph);
            //считает последовательность
            GetPath(tmpVertexes, path, pathVertexes);
        }



        // функция будет собирать путь
        private static void GetPath(List<Vertex> vertexes, (Vertex, Vertex) path, List<Vertex> pathVertex)
        {
            Vertex currentVertex = vertexes[vertexes.FindIndexForNotSorted(path.Item2)];
            pathVertex.Add(currentVertex);

            while (currentVertex.Name != path.Item1.Name)
            {
                if (currentVertex.Dad != null)
                {
                    pathVertex.Add(currentVertex.Dad);
                    currentVertex = currentVertex.Dad;
                }
                else
                {
                    throw new ExceptionDoesNotExist("Path " + path.ToString() + " does not exist");
                }
            }

            pathVertex.Reverse();
        }




        // функция которая будет записывать в таблицу пути результат построчн
        private static void GetPathTable(List<Vertex> vertexes, int[,] pathTable, int line)
        {
            vertexes.Sort();
            for (int j = 0; j < vertexes.Count; j++)
            {
                if (vertexes[j].Mark != 1000000)
                    pathTable[line, j] = vertexes[j].Mark;
                else
                    pathTable[line, j] = 0;

            }
        }



        //пересчитывает метку вершины
        private static void ChangeMark(Vertex currentVertex, Vertex currentMinMarkVertex, int ribValue)
        {
            if (currentVertex.Mark > currentMinMarkVertex.Mark + ribValue)
            {
                currentVertex.Mark = currentMinMarkVertex.Mark + ribValue;
                currentVertex.Dad = currentMinMarkVertex;
            }
        }
    }
}
