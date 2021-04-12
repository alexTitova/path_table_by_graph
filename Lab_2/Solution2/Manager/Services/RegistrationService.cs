using System;
using System.Threading.Tasks;
using AllProtos;
using Grpc.Core;
using Manager.Aplication;
using Microsoft.Extensions.Logging;

namespace Manager.Services
{
    public class RegistrationService : Registration.RegistrationBase
    {
        private readonly ILogger<RegistrationService> _logger;
        
        public RegistrationService(ILogger<RegistrationService> logger)
        {
            _logger = logger;
        }
        
        
        public override async Task<RegistResponce> Regist(RegistRequest request, ServerCallContext context)
        {
            Console.WriteLine("Registratoin worker");

            var workerMetadata = new WorkerMetadata(request.Host, request.Port);

            var isRegistered = WorkerPool.This.Add(workerMetadata);

            return await Task.FromResult(new RegistResponce {IsRegistered = isRegistered});
        }
    }
}