using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AllProtos;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;


namespace Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {

            ToRegist(args[0], Convert.ToInt32(args[1]), args[2]);
            CreateHostBuilder(args).Build().Run();
        }

        private static void ToRegist(string host, int port, string password)
        {
            using var channel = GrpcChannel.ForAddress("http://manager:5000", // use env
                new GrpcChannelOptions()
                {
                    Credentials = ChannelCredentials.Insecure,
                    LoggerFactory = new NullLoggerFactory()
                }
            );
     
            Console.WriteLine("регистрация воркера" );
                var registrationService = new Registration.RegistrationClient(channel);
                var answer = registrationService.Regist(new RegistRequest
                    {Host = host, Port = port , Password =password});
                Console.WriteLine(answer);
                
                
            


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.ConfigureKestrel(
                            options =>
                            {
                                // Setup a HTTP/2 endpoint without TLS.
                                options.ListenAnyIP(
                                    Convert.ToInt32(args[1]),
                                    o => o.Protocols = HttpProtocols.Http2
                                );
                            }
                            
                        );
                        webBuilder.UseStartup<Startup>();
                    }
                );
    }
}