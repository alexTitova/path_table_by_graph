using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllProtos;
using Google.Protobuf.Collections;
using Grpc.Core;
using Lab1Classes;
using Microsoft.Extensions.Logging;
using Worker.Parts;
using Lab1Tasks;

namespace Worker.Services
{
    public class DekstraService : Dekstra.DekstraBase
    {
        private readonly ILogger<DekstraService> _logger;

        public DekstraService(ILogger<DekstraService> logger)
        {
            _logger = logger;
        }

        public override async Task<DekResponse> Algoritm( DekRequest request, ServerCallContext context)
        {

           
            List<int> nameVerexes = new List<int>();
          
            foreach (var name in request.Names.Split(';'))
                nameVerexes.Add(Convert.ToInt32(name));

            int[,] matrix = request.MatrixAdj.ConvertRepeatedFieldStrToMatrix();
            MatrixAdjacency matrixAdjacency = new MatrixAdjacency(matrix);
            Graph graph = new Graph(nameVerexes, matrixAdjacency);

            int[,] pathTable = DekstraAlgoritm.AlgoritmPathTable(graph);



            return await Task.FromResult(
                new DekResponse
                {
                    PathTable = {pathTable.ConvertMatrixToReapetedStr()}
                });
        }
    }
}