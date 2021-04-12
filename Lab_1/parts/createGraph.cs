using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Lab1Classes;
using NLog;

namespace Lab1Parts
{
    public class CreateG
    {

        private static Logger lg = LogManager.GetCurrentClassLogger();


        private static int GetRandom(int n)
        {
            return new Random().Next(0, n);
        }


        public static Graph CreateGraph(string filePath)
        {

            SortedSet<Vertex> vertexes = new SortedSet<Vertex>();
            List<Rib> ribs = new List<Rib>();
            string line;

            lg.Debug("начал читать вершины из файла");
            using (StreamReader sr = new StreamReader(@filePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (string name in line.Split(';'))
                        vertexes.Add(new Vertex(Convert.ToInt32(name)));
                }
            }
            lg.Debug("закончил читать вершины из файла");

            Graph graph = new Graph(vertexes, ribs);

            Console.WriteLine("Add count of ribs from 0 to " + vertexes.Count*(vertexes.Count-1) +':');
            int ribsCount = Convert.ToInt32(Console.ReadLine());

            if (ribsCount < 0)
                throw new Exception("Error! Graph can not have negative count of ribs. Try program again");

            if (ribsCount > vertexes.Count * (vertexes.Count - 1))
                ribsCount = vertexes.Count * (vertexes.Count - 1);

            int i=-1;
            int j=-1;
            int ribValue;
            int n = 0;

            lg.Debug("начал создавать ребра");
            while (n < ribsCount) 
            {

                do
                {
                    i = GetRandom(graph.CountVertexes());
                    j = GetRandom(graph.CountVertexes());
                }
                while (i == j);

                ribValue = GetRandom(100);

                n++;

                try
                {
                    graph.AddRib(new Rib((vertexes.ToList()[i], vertexes.ToList()[j]), ribValue));
                }
                catch(ExceptionAlreadyExist ex)
                {
                    n--;
                    lg.Warn(ex.Message);
                }
                catch(ExceptionDoesNotExist ex)
                {
                    n--;
                    lg.Error(ex.Message + vertexes.ToList()[i]+ " or " +vertexes.ToList()[j]);
                }
                
            }

            lg.Debug("закончил создавать ребра");

            return graph;

        }

    }
}
