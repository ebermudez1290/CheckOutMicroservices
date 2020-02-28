using Microsoft.Extensions.DependencyInjection;

namespace Service.Common.Cors
{
    public static class Extension
    {
        public static void AddCORSService(this IServiceCollection services, string[] AllowedAuthOrigins)
        {
            services.AddCors(opt => opt.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(AllowedAuthOrigins);
            }));
        }
    }
}
