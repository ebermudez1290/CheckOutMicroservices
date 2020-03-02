// Sample SQL Connection Health Check
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;
using RawRabbit.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

public class RabbitMqHealthCheck : IHealthCheck
{
    public RawRabbitConfiguration _config;

    public RabbitMqHealthCheck(RawRabbitConfiguration config) { _config = config; }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
        }
        catch (DbException ex)
        {
            return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
        }
        return HealthCheckResult.Healthy();
    }
}