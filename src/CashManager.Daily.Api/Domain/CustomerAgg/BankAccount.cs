using MongoDB.Bson.Serialization.Attributes;

namespace CashManager.Daily.Api.Domain.CustomerAgg
{
    public class BankAccount
    {
        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Number")]
        public string? Number { get; set; }

        [BsonElement("Amount")]
        public decimal Amount { get; set; }

        //Current, Savings
        [BsonElement("Type")]
        public string? Type { get; set; }

        [BsonElement("Enabled")]
        public bool Enabled { get; set; }
    }
}