using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Service.Common.HC
{
    public static class Extensions
    {
        public static void AddDBHealthCheck(this IServiceCollection services, string connString)
        {
            services.AddHealthChecks().AddCheck("DB Order check", new SqlConnectionHealthCheck(connString), tags: new List<string> { "order_db" });
        }
    }
}
