using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Service.Common.Events;
using Service.Common.Serilog;
using Service.Common.Services;

namespace Audit.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggerUtil.InitApp(ServiceHost.Create<Startup>(args).UseRabbitMq()
                .SubscribeToEvent<PaymentAccepted>()
                .SubscribeToEvent<PaymentRejected>()
                .Build().Run);
            //new GrpcServer("localhost", 50051, Auditservice.AuditService.BindService(new AuditServiceImpl())).InitServer();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webbuilder => { webbuilder.UseStartup<Startup>(); });
    }
}
