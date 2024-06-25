using System;

namespace CashManager.Domain.CustomerTransactionAgg
{
    public class Transaction
    {
        public decimal Ammout { get; }
        public DateTime DateOperation { get; }
        public string? OperationType { get; }

        public Transaction(decimal ammout, DateTime dateOperation, string operationType)
        {
            Ammout = ammout;
            DateOperation = dateOperation;
            OperationType = operationType;
        }
    }
}