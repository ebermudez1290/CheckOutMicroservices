using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Eureka;
using Ocelot.Cache.CacheManager;
using Ocelot.Provider.Polly;
namespace Service.Common.Ocelot
{
    public static class Extension
    {
        public static void AddOcelotWithEureka(this IServiceCollection services)
        {
            services.AddOcelot().AddPolly().AddEureka().AddCacheManager(x => x.WithDictionaryHandle());
        }
    }
}
