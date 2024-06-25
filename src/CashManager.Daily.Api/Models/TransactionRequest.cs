using System;

namespace CashManager.Daily.Api.Models
{
    public class TransactionRequest
    {
        public decimal Ammout { get; set; }
        public string? OperationType { get; set; }
        public DateTime? DateOperation { get; set; }
    }
}