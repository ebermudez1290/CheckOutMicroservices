using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Service.Common.Serilog;

namespace Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggerUtil.InitApp(CreateHostBuilder(args).Build().Run);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("ocelot.json")
                    .AddEnvironmentVariables();
            })
            .ConfigureWebHostDefaults(webbuilder => { webbuilder.UseStartup<Startup>(); });
    }
}
