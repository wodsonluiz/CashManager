using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CashManager.Infrastructure.Mongo;
using MongoDB.Driver;

namespace CashManager.Infrastructure.Repository
{
    public class Repository<BsonDocument> : IRepository<BsonDocument>
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        
        public Repository(IMongoProvider mongoProvider, string collectionName)
        {
            _collection = mongoProvider.GetMongoDatabase().GetCollection<BsonDocument>(collectionName);
        }

        public Task Add(BsonDocument entity)
        {
            return _collection.InsertOneAsync(entity);
        }

        public Task Delete(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Id", id);

            return _collection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<BsonDocument>> GetAll()
        {
            return await _collection.Find(entity => true).ToListAsync();
        }

        public async Task<BsonDocument> GetById(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BsonDocument>> GetByFilter(Expression<Func<BsonDocument, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public Task Update(BsonDocument entity)
        {
            if(TryValueObjectToString(entity!, "Id", out string id))
            {
                 var filter = Builders<BsonDocument>.Filter.Eq("Id", id);
                return _collection.ReplaceOneAsync(filter, entity);
            }

            throw new ArgumentException($"Id: {id} invalid");
        }

        private static bool TryValueObjectToString(object entity, string key, out string value)
        {
            value = string.Empty;

            if(entity == null)
                return false;

            var type = entity.GetType();
            var property = type?.GetProperty(key);

            value = property?.GetValue(entity!, null)?.ToString();

            if(string.IsNullOrWhiteSpace(value))
                return false;

            return true;
        }
    }
}