using System;
using CashManager.Domain.ObjectValue;

namespace CashManager.Domain.ReportAgg
{
    public class ReportDaily
    {
        public string Id { get; }
        public string Name { get; }
        public string Document { get; }
        public Transaction Transaction { get; }

        public ReportDaily(string? id, string name, string document, Transaction transaction)
        {
            InvalidThrowReport(name, document, transaction);
            
            Id = id ?? Guid.NewGuid().ToString();
            Name = name;
            Document = document;
            Transaction = transaction;
        }

        public void InvalidThrowReport(string name, string document, Transaction transaction)
        {
            if(string.IsNullOrEmpty(document))
                throw new ArgumentException($"invalid field {nameof(document)} ");

            if(string.IsNullOrEmpty(name))
                throw new ArgumentException($"invalid field {nameof(name)} ");

            if(transaction == null || transaction.Amount <= 0)
                throw new ArgumentException($"invalid field {nameof(transaction)} ");
        }
    }
}