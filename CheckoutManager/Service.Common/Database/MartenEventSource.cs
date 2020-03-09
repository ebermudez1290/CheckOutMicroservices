using Grpc.Core;
using Marten;
using MongoDB.Bson;
using MongoDB.Driver;
using Service.Common.Repository.Database;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Service.Common.Repository.Database
{
    public class MartenEventSource<T> : IEventSourcingDb<T>
    {
        private IDocumentStore _store;

        public MartenEventSource(IDocumentStore store)
        {
            _store = store;
        }

        public T Create(T entity)
        {
            using (IDocumentSession session = _store.LightweightSession())
            {
                session.Store(entity);
                session.SaveChanges();
                return entity;
            }
        }

        public async Task<T> CreateAsync(T entity)
        {
            using (var session = _store.LightweightSession())
            {
                session.Store(entity);
                await session.SaveChangesAsync();
                return entity;
            }
        }

        public T GetByCriteria(Expression<Func<T, bool>> predicate)
        {
            try
            {
                using (var session = _store.QuerySession())
                {
                    return session.Query<T>().FirstOrDefault(predicate);
                }
            }
            catch (System.Exception exception)
            {
                System.Console.WriteLine(exception);
                throw;
            }
        }

        public async Task<T> GetByCriteriaAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                using (var session = _store.QuerySession())
                {
                    return await session.Query<T>().FirstOrDefaultAsync(predicate);
                }
            }
            catch (System.Exception exception)
            {
                System.Console.WriteLine(exception);
                throw;
            }
        }

        public T Update(T entity, string id)
        {
            using (var session = _store.LightweightSession())
            {
                session.Update(entity);
                session.SaveChanges();
                return entity;
            }
        }

        public void Delete(T entity)
        {
            using (var session = _store.LightweightSession())
            {
                session.Delete(entity);
                session.SaveChanges();
            }
        }

        public IQueryable<T> ListAll()
        {
            using (var session = _store.QuerySession())
            {
                return session.Query<T>().AsQueryable();
            }
        }
    }
}
