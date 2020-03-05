using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Eureka;

namespace Service.Common.Ocelot
{
    public static class Extension
    {
        public static void AddOcelotWithEureka(this IServiceCollection services, IConfiguration config)
        {
            services.AddOcelot(config).AddEureka().AddCacheManager(x => x.WithDictionaryHandle());
        }
    }
}
