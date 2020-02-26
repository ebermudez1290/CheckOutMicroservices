using Grpc.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Repository.Database
{
    class MongoDatabase<T> : IDatabase<T>
    {
        private const string connString = "mongodb+srv://ebermudez1290:blink182@cluster0-bzre0.mongodb.net/test?retryWrites=true&w=majority";
        private static MongoClient client = new MongoClient(connString);
        private static IMongoDatabase db = client.GetDatabase("MyDB");
        private static IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(typeof(T).ToString());

        public string Create(T entity)
        {
            var document = entity.ToBsonDocument();
            document.Remove("_id");
            collection.InsertOne(document);
            return document.GetValue("_id").ToString();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            try
            {
                var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("_id", new ObjectId(id));
                var result = await collection.FindAsync<T>(filter);
                if (result == null)
                    throw new RpcException(new Status(StatusCode.NotFound, $"The item with id: {id} was not found"));
                return result.FirstOrDefault();
            }
            catch (System.Exception exception)
            {
                System.Console.WriteLine(exception);
                throw;
            }
        }

        public T Update(T entity, string id)
        {
            var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("_id", new ObjectId(id)) ;
            var document = entity.ToBsonDocument();
            document.Remove("_id");
            collection.ReplaceOne(filter,document);
            return entity;
        }

        public string Delete(string id)
        {
            var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("_id", new ObjectId(id));
            var result = collection.DeleteOne(filter);
            if (result.DeletedCount == 0)
                throw new RpcException(new Status(StatusCode.NotFound, "The registry cannot be deleted"));
            return id;
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            var filter = new FilterDefinitionBuilder<BsonDocument>().Empty;
            var result = await collection.FindAsync<T>(filter);
            return result.ToList();
        }
    }
}
