using Grpc.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Service.Common.Repository.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Audit.API.Database
{
    class MongoDatabase<T> : IDatabase<T>
    {
        private const string connString = "mongodb+srv://ebermudez1290:blink182@cluster0-bzre0.mongodb.net/test?retryWrites=true&w=majority";
        private static MongoClient client = new MongoClient(connString);
        private static IMongoDatabase db = client.GetDatabase("MyDB");
        private static IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(typeof(T).ToString());

        public T Create(T entity)
        {
            var document = entity.ToBsonDocument();
            document.Remove("_id");
            collection.InsertOne(document);
            return entity;// document.GetValue("_id").ToString();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var document = entity.ToBsonDocument();
            document.Remove("_id");
            await collection.InsertOneAsync(document);
            return entity;// document.GetValue("_id").ToString();
        }

        public T GetByCriteria(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var collection = db.GetCollection<T>(typeof(T).ToString()).AsQueryable();
                var result = collection.Where(predicate);
                if (result == null)
                    throw new RpcException(new Status(StatusCode.NotFound, $"The item was not found"));
                return result.FirstOrDefault();
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
                return await Task.Run(() =>
                {
                    var collection = db.GetCollection<T>(typeof(T).ToString()).AsQueryable();
                    var result = collection.Where(predicate);
                    if (result == null)
                        throw new RpcException(new Status(StatusCode.NotFound, $"The item was not found"));
                    return result.FirstOrDefault();
                }
                );
            }
            catch (System.Exception exception)
            {
                System.Console.WriteLine(exception);
                throw;
            }
        }

        public T Update(T entity, string id)
        {
            var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("_id", new ObjectId(id));
            var document = entity.ToBsonDocument();
            document.Remove("_id");
            collection.ReplaceOne(filter, document);
            return entity;
        }

        public void Delete(T entity)
        {
            PropertyInfo prop = typeof(T).GetProperty("Id");
            string id = prop.GetValue(entity).ToString();
            var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("_id", new ObjectId(id));
            var result = collection.DeleteOne(filter);
            if (result.DeletedCount == 0)
                throw new RpcException(new Status(StatusCode.NotFound, "The registry cannot be deleted"));
        }

        public IQueryable<T> ListAll()
        {
            return db.GetCollection<T>(typeof(T).ToString()).AsQueryable();
        }
    }
}
