using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CashManager.Infrastructure.Mongo;
using MongoDB.Driver;

namespace CashManager.Infrastructure.Repository
{
    public class Repository<BsonDocument> : IRepository<BsonDocument>
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        
        public Repository(IMongoProvider mongoProvider, string collectionName) =>
            _collection = mongoProvider.GetMongoDatabase().GetCollection<BsonDocument>(collectionName);

        public Task AddAsync(BsonDocument entity, CancellationToken cancellationToken = default) =>
            _collection.InsertOneAsync(entity, null, cancellationToken);

        public async Task<IEnumerable<BsonDocument>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var documents = await _collection.FindAsync(entity => true, cancellationToken: cancellationToken);
            
            return documents?.ToList();
        }

        public async Task<BsonDocument?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            var documents = await _collection.FindAsync(filter, cancellationToken: cancellationToken);

            return documents.FirstOrDefault();
        }

        public async Task<IEnumerable<BsonDocument>> GetByFilterAsync(Expression<Func<BsonDocument, bool>> filter, 
            CancellationToken cancellationToken = default)
        {
            var documents = await _collection.FindAsync(filter, cancellationToken: cancellationToken);

            return documents.ToList();
        }
    }
}