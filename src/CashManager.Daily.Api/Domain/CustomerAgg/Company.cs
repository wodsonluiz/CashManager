using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CashManager.Daily.Api.Domain.CustomerAgg
{
    public class Company
    {
        [BsonElement("Name")]
        public string? Name { get; set; }
    }
}