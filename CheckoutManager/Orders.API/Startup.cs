using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orders.API.Configuration;
using Orders.API.Database;
using Orders.API.Models;
using Orders.API.Repository;
using Service.Common.Cors;
using Service.Common.HC;
using Service.Common.Jwt;
using Service.Common.RabbitMq.Extensions;
using Service.Common.Repository;
using Service.Common.Repository.Database;

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
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCORSService(settings.AllowedAuthOrigins);
            services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<DbContext,OrderDbContext>();
            services.AddScoped<IDatabase<Order>, EntityFrameworkDatabase<Order>>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddJWTAuthentication(settings.Secret);
            services.AddRabbitMq(Configuration.GetSection("rabbitmq"));
            services.AddDBHealthCheck(new SqlConnectionHealthCheck(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage(); else app.UseHsts();
            app.UseHealthChecks("/hc", new HealthCheckOptions() { Predicate = _ => true, });
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }

    }
}
