using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Checkout.Service.Extentions
{
    public static class ServiceExtentions
    {
        public static void AddCORSService(this IServiceCollection services, AppSettings settings)
        {
            services.AddCors(opt => opt.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(settings.AllowedAuthOrigins);
            }));
        }

        public static void AddDBHealthCheck (this IServiceCollection services, string connString)
        {
            services.AddHealthChecks().AddCheck("DB Order check", new SqlConnectionHealthCheck(connString), tags: new List<string> { "order_db" });
        }
    }
}
