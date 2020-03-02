using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Instantiation;
using Service.Common.Commands;
using Service.Common.Events;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Service.Common.RabbitMq.Extensions
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            return bus.SubscribeAsync<TCommand>(
                msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>())))
            );
        }

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            return bus.SubscribeAsync<TEvent>(
                msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>())))
            );
        }

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq(this IServiceCollection services,IConfigurationSection configuration)
        {
            var config = configuration.Get<RawRabbitConfiguration>();
            RawRabbitOptions options = new RawRabbitOptions() { ClientConfiguration = config };
            var client = RawRabbitFactory.CreateSingleton(options);
            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}
