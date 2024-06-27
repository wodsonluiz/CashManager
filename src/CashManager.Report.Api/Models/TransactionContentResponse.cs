using System;

namespace CashManager.Report.Api.Models
{
    public class TransactionContentResponse
    {
        public long Amount { get; set; }
        public DateTime DateOperation { get; set; }
        public string? OperationType { get; set; }
    }
}