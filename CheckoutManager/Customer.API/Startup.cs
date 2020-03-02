using Customer.API.Configuration;
using Customer.API.Database;
using Customer.API.EventHandlers;
using Customer.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Common.Commands;
using Service.Common.Commands.CustomerService;
using Service.Common.Cors;
using Service.Common.HC;
using Service.Common.Jwt;
using Service.Common.RabbitMq.Extensions;
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
            services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
            services.AddTransient<DbContext, CustomerDbContext>();
            services.AddTransient<IDatabase<DbModels.Customer>, EntityFrameworkDatabase<DbModels.Customer>>();
            services.AddTransient<IRepository<DbModels.Customer>, CustomerRepository>();
            services.AddTransient<ICommandHandler<CreateCustomer>, CreateCustomerHandler>();
            services.AddJWTAuthentication(settings.Secret);
            services.AddRabbitMq(Configuration.GetSection("rabbitmq"));
            services.AddDBHealthCheck(new SqlConnectionHealthCheck(connectionString));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage(); else app.UseHsts();
            app.UseHealthChecks("/hc", new HealthCheckOptions() { Predicate = _ => true, });
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
