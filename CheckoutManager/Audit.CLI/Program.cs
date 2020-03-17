using Audit.CLI.Services;
using Grpc.Core;
using System;

namespace Audit.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Channel chanel = new Channel("localhost", 61990, ChannelCredentials.Insecure);
            chanel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
                    Console.WriteLine("The client connected successfully");
            });
            var service = new AuditService().GetAll(chanel);
            Console.ReadKey();
        }
    }
}
