using Checkout.Service.Configuration;
using Checkout.Service.Database;
using Checkout.Service.Handlers;
using Checkout.Service.Models;
using Checkout.Service.PaymentGateway;
using Checkout.Service.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Common.Cors;
using Service.Common.Events;
using Service.Common.HC;
using Service.Common.RabbitMq.Extensions;
using Service.Common.Repository;
using Service.Common.Repository.Database;

namespace Checkout.Service
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) { Configuration = configuration; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            AppSettings settings = appSettingsSection.Get<AppSettings>();
            string connectionString = Configuration.GetConnectionString("PaymentDB");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCORSService(settings.AllowedAuthOrigins);
            services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(connectionString),ServiceLifetime.Singleton);
            services.AddTransient<IDatabase<Payment>, EntityFrameworkDatabase<Payment>>();
            services.AddTransient<IRepository<Payment>, PaymentRepository>();
            services.AddTransient<IEventHandler<PostedOrder>, PostedOrderHandler>();
            services.AddTransient<IPaymentGateway, PaymentGateway.PaymentGateway> ();

            services.AddRabbitMq(Configuration.GetSection("rabbitmq"));
            services.AddDBHealthCheck(connectionString);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage(); else app.UseHsts();
            app.UseHealthChecks("/hc", new HealthCheckOptions() { Predicate = _ => true, });
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
