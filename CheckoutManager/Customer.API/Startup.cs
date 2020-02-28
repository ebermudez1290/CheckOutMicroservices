using Customer.API.Configuration;
using Customer.API.Database;
using Customer.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Common.Cors;
using Service.Common.HC;
using Service.Common.Jwt;
using Service.Common.Repository;
using Service.Common.Repository.Database;
using DbModels = Customer.API.Models;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCORSService(settings.AllowedAuthOrigins);
            services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IDatabase<DbModels.Customer>, EntityFrameworkDatabase<DbModels.Customer>>();
            services.AddScoped<IRepository<DbModels.Customer>, CustomerRepository>();
            services.AddJWTAuthentication(settings.Secret);
            services.AddDBHealthCheck(connectionString);
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
