using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audit.API.Configuration;
using Audit.API.Database;
using Audit.API.Handlers;
using Audit.API.Repository;
using Auditservice;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Common.Cors;
using Service.Common.Events;
using Service.Common.HC;
using Service.Common.RabbitMq.Extensions;
using Service.Common.Repository;
using Service.Common.Repository.Database;

namespace Audit.API
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
            string connectionString = Configuration.GetConnectionString("AuditDB");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCORSService(settings.AllowedAuthOrigins);
            services.AddTransient<IDatabase<AuditEntry>, MongoDatabase<AuditEntry>>();
            services.AddTransient<IRepository<AuditEntry>, AuditRepository>();
            services.AddTransient<IEventHandler<PaymentAccepted>, PaymentAcceptedHandler>();
            services.AddTransient<IEventHandler<PaymentRejected>, PaymentRejectedHandler>();

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
