using Checkout.Service.Database;
using Checkout.Service.Extentions;
using Checkout.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Common.Repository.Database;

namespace Checkout.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            AppSettings settings = appSettingsSection.Get<AppSettings>();
            string connectionString = Configuration.GetConnectionString("PaymentDB");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCORSService(settings);
            services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IDatabase<Payment>, EntityFrameworkDatabase<Payment>>();
            //services.AddScoped<IRepository<Payment>, PaymentRepository>();
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
