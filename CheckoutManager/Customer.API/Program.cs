
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webbuilder =>{ webbuilder.UseStartup<Startup>();});
    }
}
