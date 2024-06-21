using MongoDB.Driver;

namespace CashManager.Daily.Api.Infrastructure.Mongo
{
    public interface IMongoProvider
    {
        IMongoDatabase GetMongoDatabase();
    }
}