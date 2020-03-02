using Auditservice;
using Service.Common.Repository;
using Service.Common.Repository.Database;
using System.Linq;
using System.Text;
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

        public async Task<AuditEntry> CreateAsync(AuditEntry entity)
        {
            return await _db.CreateAsync(entity);
        }

        public async Task<AuditEntry> GetByIdAsync(string id)
        {
            return await _db.GetByCriteriaAsync(x => Encoding.UTF8.GetString(x.Id.ToByteArray()) == id);
        }

        public AuditEntry Update(AuditEntry entity)
        {
            return _db.Update(entity, Encoding.UTF8.GetString(entity.Id.ToByteArray()));
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
