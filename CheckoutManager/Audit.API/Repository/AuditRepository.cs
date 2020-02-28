using Auditservice;
using Service.Common.Repository;
using Service.Common.Repository.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Audit.API.Repository
{
    public class AuditRepository : IRepository<AuditEntry>
    {
        private IDatabase<AuditEntry> _db;
        public AuditRepository(IDatabase<AuditEntry> db)
        {
            _db = db;
        }

        public AuditEntry Create(AuditEntry entity)
        {
            return _db.Create(entity);
        }

        public async Task<AuditEntry> GetByIdAsync(string id)
        {
            return await _db.GetByCriteriaAsync(x=>x.Id == id);
        }

        public AuditEntry Update(AuditEntry entity)
        {
            return _db.Update(entity, entity.Id);
        }

        public void Delete(AuditEntry entity)
        {
            _db.Delete(entity);
        }

        public IQueryable<AuditEntry> ListAllAsync()
        {
            return _db.ListAll();
        }

    }
}
