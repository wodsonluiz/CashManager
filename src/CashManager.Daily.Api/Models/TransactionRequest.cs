using System;

namespace CashManager.Daily.Api.Models
{
    public class TransactionRequest
    {
        public long Amount { get; set; }
        public string OperationType { get; set; }
        public DateTime? DateOperation { get; set; }
    }
}