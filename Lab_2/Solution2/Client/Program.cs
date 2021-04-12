using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic;
using AllProtos;
using Interaction = AllProtos.Interaction;

namespace Client
{
    class Program
    {
        static  async Task  Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(
                "http://localhost:5000",
                new GrpcChannelOptions()
                {
                    Credentials = ChannelCredentials.Insecure,
                    LoggerFactory = new NullLoggerFactory()
                }
            );
            
            Console.WriteLine("Add path to file with data:");
            string filePath = Console.ReadLine();
            string names = Read.ReadFromCsv(filePath);
            Console.WriteLine("Add count of ribs. It must be bigger then zero");
            int count = Convert.ToInt32(Console.ReadLine());

            var interactionService = new Interaction.InteractionClient(channel);
            var graphTables = interactionService.CreateG(
                new NameRequest
                {
                    Names = names, 
                    Count = count
                });
            
            Console.WriteLine("Add path to file to write any tables:");
            string filePath1 = Console.ReadLine();
            
            Task writeMA = new Task( () => Write.WriteTablesToCsv(filePath1, "Matrix Adjacency",graphTables.MatrixAdj) );
            writeMA.Start();
           
           
          Task writePT = new Task( () =>
          {
              var pathTable = interactionService.DekstraAlgoritm(
                  new AlgoritmRequest
                  {
                      Names = names,
                      GraphMatrixAgj = {graphTables.MatrixAdj}
                  });
              
              writeMA.Wait();
              Write.WriteTablesToCsv(filePath1, "Path TAble", pathTable.PathTab);
          });
          writePT.Start();

            Console.WriteLine("Path Table wrote");
            Console.WriteLine("Add path to file to write path sequences:");
            string filePath2 = Console.ReadLine();
            Console.WriteLine("Do you want to know path vertexes");
            string answer = Console.ReadLine();

            do
            {
                int start;
                int end;
                bool flag;

                if (answer == "Да" || answer == "да" || answer == "Yes" || answer == "yes")
                {
                    Console.WriteLine("добавьте имена вершин между которыми хотите узнать путь");
                    start = Convert.ToInt32(Console.ReadLine());
                    end = Convert.ToInt32(Console.ReadLine());
                    flag = true;
                }
                else
                {
                    start = 0;
                    end = 0;
                    flag = false;
                }

                var vertexSequence = interactionService.VeretxSequence(
                    new VertexRequest
                    {
                        IsNeeded = flag,
                        StartVertex = start,
                        EndVertex = end,
                        Names = names,
                        GraphMatrixAdj = {graphTables.MatrixAdj}
                    });
                
                Write.WritePathSeqToCsv(filePath2, "vertex path sequence", vertexSequence.VertexSequence);
                Console.WriteLine("Do you want to know path vertexes");
                answer = Console.ReadLine();
                
            } while (answer == "Yes" || answer == "yes" || answer == "Да" || answer == "да");

            await Task.WhenAll(writePT);

        }
    }
}