using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllProtos;
using Grpc.Core;
using Lab1Classes;
using Microsoft.Extensions.Logging;
using Lab1Tasks;
using Worker.Parts;

namespace Worker.Services
{
    public class VertexSeqService : VertexSeq.VertexSeqBase
    {
        private readonly ILogger<VertexSeqService> _logger;

        public VertexSeqService(ILogger<VertexSeqService> logger)
        {
            _logger = logger;
        }

        public override async Task<VertResponse> GetVertexPath(VertRequest request, ServerCallContext context)
        {
            int[,] matrix = request.GraphMatrixAdj.ConvertRepeatedFieldStrToMatrix();
            
            List<int> vertexNames = new List<int>();

            foreach (var name in request.Names.Split(';'))
                vertexNames.Add(Convert.ToInt32(name));

            Graph graph = new Graph(vertexNames, new MatrixAdjacency(matrix));

            string result = DekstraAlgoritm.AlgoritmListPath(graph, request.StartVertex, request.EndVertex);

            return await Task.FromResult(
                new VertResponse
                {
                    VertexSequence = result
                });
        }
    }
}