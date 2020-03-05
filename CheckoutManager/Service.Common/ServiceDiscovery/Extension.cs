using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Discovery.Client;

namespace Service.Common.ServiceDiscovery
{
    public static class Extension
    {
        public static void AddServiceDiscovery (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDiscoveryClient(configuration);
        }
    }
}
