using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Eureka;
using Ocelot.Cache.CacheManager;
namespace Service.Common.Ocelot
{
    public static class Extension
    {
        public static void AddOcelotWithEureka(this IServiceCollection services)
        {
            services.AddOcelot().AddEureka().AddCacheManager(x => x.WithDictionaryHandle());
        }
    }
}
