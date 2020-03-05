using Gateway.API.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Service.Common.Cors;
using Service.Common.Jwt;
using Service.Common.Ocelot;
using Service.Common.RabbitMq.Extensions;
using System;

namespace Gateway.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfigurationRoot ConfigurationRoot { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env) {
            Configuration = configuration;
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            ConfigurationRoot = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            AppSettings settings = appSettingsSection.Get<AppSettings>();
            services.AddCORSService(settings.AllowedAuthOrigins);
            services.AddOcelotWithEureka(ConfigurationRoot);
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage(); else app.UseHsts();
            app.UseCors("CorsPolicy");
            await app.UseOcelot();
        }
    }
}
