using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Service.Common.Events;
using Service.Common.Serilog;
using Service.Common.Services;

namespace Checkout.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggerUtil.InitApp(ServiceHost.Create<Startup>(args).UseRabbitMq().SubscribeToEvent<PostedOrder>().Build().Run);
        }

        public static IHostBuilder BuildWebHost(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webbuilder => { webbuilder.UseStartup<Startup>(); });
    }
}
