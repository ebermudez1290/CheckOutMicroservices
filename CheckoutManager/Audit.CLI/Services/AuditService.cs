

using Auditservice;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audit.CLI.Services
{
    class AuditService : IAuditService
    {
        public async Task GetAll(Channel channel)
        {
            try
            {
                var client = new Auditservice.AuditService.AuditServiceClient(channel);
                var result = client.ReadAll(new ReadAllAuditRequest());
                List<AuditEntry> entries = new List<AuditEntry>();
                while (await result.ResponseStream.MoveNext())
                    entries.Add(result.ResponseStream.Current.AuditResponse);
                System.Console.WriteLine($"Here is the audit list: {string.Join(",", entries)} {Environment.NewLine}");
            }
            catch (RpcException)
            {

                throw;
            }
        }
    }
}
