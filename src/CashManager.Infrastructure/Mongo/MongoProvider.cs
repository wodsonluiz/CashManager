using System;
using MongoDB.Driver;

namespace CashManager.Infrastructure.Mongo
{
    public class MongoProvider: IMongoProvider
    {
        private readonly IMongoClient _client;
        private readonly MongoOptions _options;
        public MongoProvider(string? conn, MongoOptions options)
        {
            if(string.IsNullOrWhiteSpace(conn))
                throw new ArgumentException("conn are invalid", conn);

            _client = new MongoClient(conn);
            _options = options ?? throw new ArgumentException("conn are invalid", conn);
        }

        public IMongoDatabase GetMongoDatabase() => _client.GetDatabase(_options.DatabaseName);
    }
}