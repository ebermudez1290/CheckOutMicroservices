using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RawRabbit;
using Service.Common.Commands;
using Service.Common.Events;
using Service.Common.RabbitMq.Extensions;
using System;

namespace Service.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IHost _webHost;

        public ServiceHost(IHost webHost)
        {
            this._webHost = webHost;
        }

        public void Run() => this._webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            var config = new ConfigurationBuilder().AddEnvironmentVariables().AddCommandLine(args).Build();

            var host = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webbuilder =>
            {
                webbuilder.UseStartup<TStartup>();
            }).Build();
            return new HostBuilder(host);
        }
    }

    public abstract class BuilderBase
    {
        public abstract ServiceHost Build();
    }

    public class HostBuilder : BuilderBase
    {
        private readonly IHost _webHost;
        private IBusClient _bus;

        public HostBuilder(IHost webHost) { this._webHost = webHost; }

        public BusBuilder UseRabbitMq()
        {
            this._bus = (IBusClient)this._webHost.Services.GetService(typeof(IBusClient));
            return new BusBuilder(_webHost, _bus);
        }

        public override ServiceHost Build() => new ServiceHost(_webHost);
    }

    public class BusBuilder : BuilderBase
    {
        private readonly IHost _webHost;
        private IBusClient _bus;

        public BusBuilder(IHost webHost, IBusClient bus)
        {
            this._webHost = webHost;
            this._bus = bus;
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_webHost.Services.GetService(typeof(ICommandHandler<TCommand>));
            _bus.WithCommandHandlerAsync(handler);
            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            var handler = (IEventHandler<TEvent>)_webHost.Services.GetService(typeof(IEventHandler<TEvent>));
            _bus.WithEventHandlerAsync(handler);
            return this;
        }

        public override ServiceHost Build() => new ServiceHost(_webHost);
    }

}