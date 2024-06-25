using MongoDB.Driver;

namespace CashManager.Infrastructure.Mongo
{
    public interface IMongoProvider
    {
        IMongoDatabase GetMongoDatabase();
    }
}