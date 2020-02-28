using System.Linq;
using System.Threading.Tasks;
using Audit.API.Database;
using Audit.API.Repository;
using Auditservice;
using Grpc.Core;
using Service.Common.Repository;
using Service.Common.Repository.Database;
using static Auditservice.AuditService;

namespace Audit.API.ServiceImpl
{
    public class AuditServiceImpl : AuditServiceBase
    {

        public async override Task<CreateAuditResponse> Create(CreateAuditRequest request, ServerCallContext context)
        {
            IDatabase<AuditEntry> database = new MongoDatabase<AuditEntry>();
            IRepository<AuditEntry> repository = new AuditRepository(database);
            var result = repository.Create(request.Audit);
            return await Task.FromResult(new CreateAuditResponse() { AuditResponse = result });
        }

        public async override Task<DeleteAuditResponse> Delete(DeleteAuditRequest request, ServerCallContext context)
        {
            IDatabase<AuditEntry> database = new MongoDatabase<AuditEntry>();
            IRepository<AuditEntry> repository = new AuditRepository(database);
            repository.Delete(request.Audit);
            return await Task.FromResult(new DeleteAuditResponse() { AuditResponse = request.Audit });
        }

        public async override Task<ReadAuditResponse> Read(ReadAuditRequest request, ServerCallContext context)
        {
            IDatabase<AuditEntry> database = new MongoDatabase<AuditEntry>();
            IRepository<AuditEntry> repository = new AuditRepository(database);
            var result = await repository.GetByIdAsync(request.Audit.Id);
            return await Task.FromResult(new ReadAuditResponse() { AuditResponse = result });
        }

        public async override Task ReadAll(ReadAllAuditRequest request, IServerStreamWriter<ReadAllAuditResponse> responseStream, ServerCallContext context)
        {
            IDatabase<AuditEntry> database = new MongoDatabase<AuditEntry>();
            IRepository<AuditEntry> repository = new AuditRepository(database);
            var result = repository.ListAllAsync().ToList();
            foreach (var auditEntry in result)
            {
                await responseStream.WriteAsync(new ReadAllAuditResponse() { AuditResponse = auditEntry });
            }
        }

        public async override Task<UpdateAuditResponse> Update(UpdateAuditRequest request, ServerCallContext context)
        {
            IDatabase<AuditEntry> database = new MongoDatabase<AuditEntry>();
            IRepository<AuditEntry> repository = new AuditRepository(database);
            var result = repository.Update(request.Audit);
            return await Task.FromResult(new UpdateAuditResponse() { AuditResponse = result });
        }
    }
}
