using System;

namespace CashManager.Domain.TransactionAgg
{
    public class Transaction
    {
        public string? Id { get; set; } 

        public DateTime Date { get; set; }

        public string? CustomerDocumentNumber { get; set; }

        //Debit, Credit  
        public string? TypeOperation { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }
    }
}