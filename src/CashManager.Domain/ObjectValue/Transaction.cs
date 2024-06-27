using System;

namespace CashManager.Domain.ObjectValue
{
    public class Transaction
    {
        public long Amount { get; }
        public DateTime DateOperation { get; }
        public string OperationType { get; }

        public Transaction(long amount, DateTime dateOperation, string operationType)
        {
            Amount = amount;
            DateOperation = dateOperation;
            OperationType = operationType;
        }
    }
}