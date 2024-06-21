using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CashManager.Daily.Api.Domain.CustomerAgg
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Document")]
        public string? Document { get; set; }

        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Email")]
        public string? Email { get; set; }

        [BsonElement("Pass")]
        public string? Pass { get; set; }

        //Administrator, Common user
        [BsonElement("Profile")]
        public string? Profile { get; set; }

        [BsonElement("Company")]
        public Company? Company { get; set; }

        [BsonElement("BankAccount")]
        public List<BankAccount>? BankAccount { get; set; }
    }
}