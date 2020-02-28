using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using Service.Common.Serilog;
using Service.Common.Services;
using System;

namespace Orders.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggerUtil.InitApp(ServiceHost.Create<Startup>(args).UseRabbitMq().Build().Run);
        }

        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}
