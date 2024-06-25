using System;

namespace CashManager.Domain.CustomerTransactionAgg
{
    public class Company
    {
        public string? Name { get; }

        public Company(string? name) =>
            Name = name ?? throw new ArgumentException($"invalid field {nameof(name)}");
    }
}