using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orders.API.Configuration;
using Orders.API.Database;
using Orders.API.Models;
using Orders.API.Repository;
using Service.Common.Cors;
using Service.Common.Events;
using Service.Common.EventSourcing;
using Service.Common.HC;
using Service.Common.Jwt;
using Service.Common.RabbitMq.Extensions;
using Service.Common.Repository;
using Service.Common.Repository.Database;
using Service.Common.ServiceDiscovery;
using Steeltoe.Discovery.Client;

namespace Orders.API
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
            string connectionString = Configuration.GetConnectionString("OrderDB");

            services.AddControllers().AddNewtonsoftJson(opt => { opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });
            //Cors
            services.AddCORSService(settings.AllowedAuthOrigins);
            //Database access
            services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<DbContext, OrderDbContext>();
            services.AddScoped<IDatabase<Order>, EntityFrameworkDatabase<Order>>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            //Marten add
            //services.AddMartenEventSourcing(Configuration.GetConnectionString("EventSource"));
            //services.AddScoped<IEventSourcingDb<PostedOrder>, MartenEventSource<PostedOrder>>();
            //JWT
            services.AddJWTAuthentication(settings.Secret);
            //RabbitMQ
            services.AddRabbitMq(Configuration.GetSection("rabbitmq"));
            //HC
            services.AddDBHealthCheck(new SqlConnectionHealthCheck(connectionString));
            services.AddServiceDiscovery(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage(); else app.UseHsts();
            app.UseRouting();
            app.UseHealthChecks("/hc", new HealthCheckOptions() { Predicate = _ => true, });
            app.UseCors("CorsPolicy");
            app.UseDiscoveryClient();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

    }
}
