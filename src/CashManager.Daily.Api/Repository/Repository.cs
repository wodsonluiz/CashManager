using System.Threading.Tasks;
using CashManager.Daily.Api.Infrastructure.Mongo;
using MongoDB.Driver;

namespace CashManager.Daily.Api.Repository.Customer
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public Repository(MongoProvider mongoProvider, string collectionName)
        {
            _collection = mongoProvider.GetMongoDatabase().GetCollection<T>(collectionName);
        }

        public Task Add(T entity)
        {
            return _collection.InsertOneAsync(entity);
        }
    }
}