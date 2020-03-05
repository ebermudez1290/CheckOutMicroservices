using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Service.Common.Serilog;
using Service.Common.Services;

namespace Customer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggerUtil.InitApp(ServiceHost.Create<Startup>(args).Build().Run);
        }

        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}
