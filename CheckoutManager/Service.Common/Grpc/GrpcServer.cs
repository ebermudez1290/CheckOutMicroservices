using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Common.Grpc
{
    public class GrpcServer : IGrpcServer
    {
        private readonly int _port;
        private readonly string _serverUrl;
        private readonly ServerServiceDefinition _services;

        public GrpcServer(string serverUrl, int port, ServerServiceDefinition services)
        {
            _port = port;
            _services = services;
            _serverUrl = serverUrl;
        }

        public void InitServer()
        {
            Server server = null;
            try
            {
                server = new Server()
                {
                    Services = { _services },
                    Ports = { new ServerPort(_serverUrl, _port, ServerCredentials.Insecure) }
                };
                server.Start();
                Console.ReadKey();
            }
            finally
            {
                server?.ShutdownAsync().Wait();
            }
        }
    }
}
