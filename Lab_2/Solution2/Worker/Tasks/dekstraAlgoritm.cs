using Lab1Classes;
using Lab1Parts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace Lab1Tasks
{
    public class DekstraAlgoritm
    {
        



        // функция где будет распарлеливаться вычисления и собераться вместе 
        // возврашать должна таблицу расстояний+последовательность вершин рути между указанными то  есть пару (int[,], string)
        public static int[,] AlgoritmPathTable(Graph graph)
        {
            int[,] pathTable = new int[graph.CountVertexes(), graph.CountVertexes()];

            Parallel.ForEach(graph.Vertexes, start => ForLineDekstra(graph, start, pathTable));


            return pathTable;
        }

        
        public static string AlgoritmListPath(Graph graph, int start, int end)
        {
           
                List<Vertex> pathVertexes = new List<Vertex>();
                string result = "";
                Vertex startV = new Vertex(start);
                Vertex endV = new Vertex(end);

                if (graph.Vertexes.Contains(startV) && graph.Vertexes.Contains(endV))
                {
                    
                    try
                    {
                        result=   GetVertexesPath(graph,( startV, endV), pathVertexes);
                    }
                    catch (ExceptionDoesNotExist ex)
                    {
                        result = "отсутствует путь " + start.ToString() + " - " + end.ToString();
                    }

                }
                else
                { 
                    result ="отсутствует вершина " + start.ToString() + " или " + end.ToString();
                }

                return result;
        }



        //вычисляется и собирается строка таблицы пути
        private static void ForLineDekstra(Graph graph, Vertex start, int[,] pathTable)
        {
       
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

        

        private static string GetVertexesPath(Graph graph, (Vertex, Vertex) path, List<Vertex> pathNamesVertexes)
        {
            string result = "";
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
            result= GetPath(tmpVertexes, path, pathNamesVertexes);
            return result;
        }



        // функция будет собирать путь
        private static string GetPath(List<Vertex> vertexes, (Vertex, Vertex) path, List<Vertex> pathNamesVertex)
        {
            string result = "";
            Vertex currentVertex = vertexes[vertexes.FindIndexForNotSorted(path.Item2)];
            pathNamesVertex.Add(currentVertex);

            while (currentVertex.Name != path.Item1.Name)
            {
                if (currentVertex.Dad != null)
                {
                    pathNamesVertex.Add(currentVertex.Dad);
                    currentVertex = currentVertex.Dad;
                }
                else
                {
                    throw new ExceptionDoesNotExist("Path " + path.ToString() + " does not exist");
                }
            }

            pathNamesVertex.Reverse();
        //    result = pathNamesVertex.ToString();
        foreach (var vertex in pathNamesVertex)
            result += vertex.ToString()+ " - ";
        
            return result;
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
