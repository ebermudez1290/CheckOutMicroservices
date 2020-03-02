using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RawRabbit.Configuration;
using RawRabbit.Instantiation;
using System.Collections.Generic;

namespace Service.Common.HC
{
    public static class Extensions
    {
        public static void AddDBHealthCheck(this IServiceCollection services, IHealthCheck healthCheck)
        {
            services.AddHealthChecks().AddCheck("DB check", healthCheck, tags: new List<string> { "DB check" });
        }

        public static void AddRabbitMqHealthCheck(this IServiceCollection services, IConfigurationSection configuration)
        {
            var config = configuration.Get<RawRabbitConfiguration>();
            RawRabbitOptions options = new RawRabbitOptions() { ClientConfiguration = config };
            services.AddHealthChecks().AddCheck("RabbitMq check", new RabbitMqHealthCheck(config), tags: new List<string> { "order queue", "payment queue" });
        }
    }
}
