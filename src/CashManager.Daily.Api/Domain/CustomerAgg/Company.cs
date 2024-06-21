using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CashManager.Daily.Api.Domain.CustomerAgg
{
    public class Company
    {
        [BsonElement("Id")]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string? Name { get; set; }
    }
}