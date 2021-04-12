using System;
using System.Collections.Concurrent;
using System.Linq;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging.Abstractions;

namespace Manager.Aplication
{
    public class WorkerPool
    {
        private ConcurrentDictionary<int, (WorkerMetadata Metadata, GrpcChannel Channel)> _workers;

        private WorkerPool()
        {
            this._workers =
                new ConcurrentDictionary<int, (WorkerMetadata Metadata, GrpcChannel Channel)>();
        }


        public bool Add(WorkerMetadata workersMetadata)
        {
            var channel = GrpcChannel.ForAddress(
                $"http://{workersMetadata.Host }:{workersMetadata.Port}",
                new GrpcChannelOptions()
                {
                    Credentials = ChannelCredentials.Insecure,
                    LoggerFactory = new NullLoggerFactory()
                });
            
            this._workers.TryAdd(workersMetadata.GetHashCode(), (workersMetadata, channel));
            return true;

        }
        
        
        public void Remove(WorkerMetadata workerMetadata)
        {
            this._workers.TryRemove(workerMetadata.GetHashCode(), out var removed);
            removed.Channel.Dispose();
        }
        
        
        public static WorkerPool This { get; private set; }

        public static void Init() { This = new WorkerPool(); }
        
        
        
        public GrpcChannel GetRandom()
        {
            var workerMetadatas = this._workers.Values.ToList();
            if (workerMetadatas.Count == 0) { throw new Exception("No workers!"); }
            var rnd = new Random(DateTime.Now.Millisecond);
            var index = rnd.Next(0, workerMetadatas.Count - 1);
            var target = workerMetadatas[index];
            return target.Channel;
        }




        
        
    }
}