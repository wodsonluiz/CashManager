using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CashManager.Daily.Api.Infrastructure.Mongo;
using CashManager.Daily.Api.Shared;
using MongoDB.Driver;

namespace CashManager.Daily.Api.Repository.Customer
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        
        public Repository(IMongoProvider mongoProvider, string collectionName)
        {
            _collection = mongoProvider.GetMongoDatabase().GetCollection<T>(collectionName);
        }

        public Task Add(T entity)
        {
            return _collection.InsertOneAsync(entity);
        }

        public Task Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            return _collection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(entity => true).ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public Task Update(T entity)
        {
            if(entity.TryValueObjectToString("Id", out string id))
            {
                 var filter = Builders<T>.Filter.Eq("Id", id);
                return _collection.ReplaceOneAsync(filter, entity);
            }

            throw new ArgumentException($"Id: {id} invalid");
        }
    }
}