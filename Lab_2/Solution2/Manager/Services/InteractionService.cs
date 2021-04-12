using System;
using System.Threading.Tasks;
using AllProtos;
using Google.Protobuf.Collections;
using Grpc.Core;
using Manager.Aplication;
using Microsoft.Extensions.Logging;

namespace Manager.Services
{
    public class InteractionService : Interaction.InteractionBase
    {
        private readonly ILogger<InteractionService> _logger;
        private RepeatedField<string> graphMatrixAgjacency;
        private string names;
        
        public InteractionService(ILogger<InteractionService> logger)
        {
            _logger = logger;
        }

        public override async Task<MatrixResponse> CreateG(NameRequest request, ServerCallContext context)
        {
            Console.WriteLine("manager create graph");
            var channel = WorkerPool.This.GetRandom();
            Console.WriteLine("Connecting to worker");
            Console.WriteLine(channel.ToString() );

            var creatingGraphService = new CreartingGraph.CreartingGraphClient(channel);

            var matrixAdjResponce = creatingGraphService.Creating (
                new GraphRequest 
                {
                    Names = request.Names, 
                    Count = request.Count
                });
            
            Console.WriteLine("Graph Created");
            this.graphMatrixAgjacency = matrixAdjResponce.Matrix;
            this.names = request.Names;


            return await Task.FromResult(
                new MatrixResponse
                {
                    MatrixAdj = { matrixAdjResponce.Matrix}
                });
        }

        public override async Task<AlgoritmResponse> DekstraAlgoritm(AlgoritmRequest request, ServerCallContext context)
        {
            Console.WriteLine("Start path table");
            var channel = WorkerPool.This.GetRandom();
            Console.WriteLine("Connecting to worker");
            
            var dekstraService = new Dekstra.DekstraClient(channel);
            var pathTableResponse = dekstraService.Algoritm(
                new DekRequest
                {
                    MatrixAdj = {request.GraphMatrixAgj},
                    Names = request.Names
                });
            

            Console.WriteLine("End path table");

            return await Task.FromResult(
                new AlgoritmResponse
                {
                    PathTab = {pathTableResponse.PathTable}
                });
        }

        public override async Task<VertexResponse> VeretxSequence(VertexRequest request, ServerCallContext context)
        {
            Console.WriteLine("manager create path vertex");
           
            string result = "";
            if (request.IsNeeded)
            {
                var channel = WorkerPool.This.GetRandom();
                Console.WriteLine("Connecting to worker");

                var vertexSeqService = new VertexSeq.VertexSeqClient(channel);
                
                var sequenseResponse = vertexSeqService.GetVertexPath(
                    new VertRequest
                    {
                        StartVertex = request.StartVertex,
                        EndVertex = request.EndVertex,
                        Names = request.Names,
                        GraphMatrixAdj = {request.GraphMatrixAdj}

                    });

                result = sequenseResponse.VertexSequence;
            }
            else
                result = "клиент отказался узнать путь";



            return await Task.FromResult(
                new VertexResponse
                {
                    VertexSequence = result
                });
        }
    }
}