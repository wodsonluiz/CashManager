using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CashManager.Daily.Api.Domain
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("CustomerDocumentNumber")]
        public string? CustomerDocumentNumber { get; set; }

        //Debit, Credit  
        [BsonElement("TypeOperation")]
        public string? TypeOperation { get; set; }

        [BsonElement("Amount")]
        public decimal Amount { get; set; }

        [BsonElement("Description")]
        public string? Description { get; set; }
    }
}