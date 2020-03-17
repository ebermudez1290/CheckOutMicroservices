using AutoMapper;
using Customer.API.Configuration;
using Customer.API.Database;
using Customer.API.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Common.Cors;
using Service.Common.HC;
using Service.Common.Jwt;
using Service.Common.Repository;
using Service.Common.Repository.Database;
using Service.Common.ServiceDiscovery;
using Steeltoe.Discovery.Client;
using System.Reflection;
using DbModels = Customer.API.Models;
using Service.Common.AutoMapper;

namespace Customer.API
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
            string connectionString = Configuration.GetConnectionString("CustomerDB");

            services.AddControllers().AddNewtonsoftJson(opt => { opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });
            services.AddCORSService(settings.AllowedAuthOrigins);
            services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, CustomerDbContext>();
            services.AddScoped<IDatabase<DbModels.Customer>, EntityFrameworkDatabase<DbModels.Customer>>();
            services.AddScoped<IRepository<DbModels.Customer>, CustomerRepository>();
            services.AddJWTAuthentication(settings.Secret);
            services.AddDBHealthCheck(new SqlConnectionHealthCheck(connectionString));
            services.AddServiceDiscovery(Configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapperSupport(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage(); else app.UseHsts();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseHealthChecks("/hc", new HealthCheckOptions() { Predicate = _ => true, });
            app.UseDiscoveryClient();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
