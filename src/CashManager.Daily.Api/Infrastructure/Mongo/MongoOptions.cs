namespace CashManager.Daily.Api.Infrastructure.Mongo
{
    public class MongoOptions
    {
        public const string PREFIX = "MongoSettings";

        public string? DatabaseName { get; set; }
    }
}