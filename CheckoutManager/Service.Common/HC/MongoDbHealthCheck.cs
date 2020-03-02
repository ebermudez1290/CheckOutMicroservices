// Sample SQL Connection Health Check
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

public class MongoDbHealthCheck : IHealthCheck
{
    public string ConnectionString { get; }

    public MongoDbHealthCheck(string connectionString) { ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString)); }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var mongoClient = new MongoClient(ConnectionString);
            var database = mongoClient.GetDatabase("MyDB");
            var isMongoLive = (database.RunCommandAsync((Command<BsonDocument>)"{ping:1}")).Wait(1000);
            return isMongoLive ? HealthCheckResult.Healthy() : throw new ArgumentException("Mongo DB is not available");
        }
        catch (Exception ex)
        {
            return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
        }
    }
}