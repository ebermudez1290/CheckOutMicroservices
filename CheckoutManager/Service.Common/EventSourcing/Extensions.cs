using Marten;
using Marten.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Service.Common.Events;
using Service.Common.Repository.Database;
using System;

namespace Service.Common.EventSourcing
{
    public static class Extension
    {
        public static void AddMartenEventSourcing(this IServiceCollection services, string cnnString)
        {
            services.AddSingleton<IDocumentStore>(CreateDocumentStore(cnnString));
        }

        private static IDocumentStore CreateDocumentStore(string cn)
        {
            return DocumentStore.For(_ =>
            {
                _.Connection(cn);
                _.DatabaseSchemaName = "PostedOrder";
                _.Serializer(CustomizeJsonSerializer());
                _.Schema.For<PostedOrder>();
            });
        }

        private static JsonNetSerializer CustomizeJsonSerializer()
        {
            var serializer = new JsonNetSerializer();

            serializer.Customize(_ =>
            {
                _.ContractResolver = new ProtectedSettersContractResolver();
                _.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            });

            return serializer;
        }
    }
}
