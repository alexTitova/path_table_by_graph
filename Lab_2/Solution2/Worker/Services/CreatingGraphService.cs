using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllProtos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Lab1Classes;
using Microsoft.Extensions.DependencyInjection;
using Worker.Parts;

namespace Worker.Services
{
    public class CreatingGraphService : CreartingGraph.CreartingGraphBase
    {
        private readonly ILogger<CreatingGraphService> _logger;

        public CreatingGraphService(ILogger<CreatingGraphService> logger)
        {
            _logger = logger;
        }


        public override async Task<GraphResponse> Creating(GraphRequest request, ServerCallContext context)
        {
            SortedSet<Vertex> vertexes = new SortedSet<Vertex>();
            foreach (var name in request.Names.Split(';'))
                vertexes.Add(new Vertex(Convert.ToInt32(name)));

            List<Rib> ribs = new List<Rib>();
            
            Graph graph = new Graph(vertexes, ribs);
            
            if (request.Count <0)
                throw new Exception("Error! Graph can not have negative count of ribs. Try program again");

            int count = 0;
            if (request.Count > vertexes.Count * (vertexes.Count - 1))
                count = vertexes.Count * (vertexes.Count - 1);
            else
                count = request.Count;
            
            int i=-1;
            int j=-1;
            int ribValue;
            int n = 0;
            
            while (n < count) 
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
                catch (ExceptionAlreadyExist ex)
                {
                    n--;
                    Console.WriteLine("WARN: " + ex.Message);
                }
                catch (ExceptionDoesNotExist ex)
                {
                    n--;
                    Console.WriteLine("ERROR: " + ex.Message + vertexes.ToList()[i] + " or " + vertexes.ToList()[j]);
                }

            }

            int[,] matrix = graph.GetMatrixAdjacency().Data;

            return await Task.FromResult(
                new GraphResponse
                {
                    Matrix = {matrix.ConvertMatrixToReapetedStr()}
                });
        }
        
        private static int GetRandom(int n)
        {
            return new Random().Next(0, n);
        }
        

    }
}