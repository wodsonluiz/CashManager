namespace CashManager.Domain.CustomerAgg
{
    public class BankAccount
    {
        public string? Name { get; set; }

        public string? Number { get; set; }

        public decimal Amount { get; set; }

        //Current, Savings
        public string? Type { get; set; }

        public bool Enabled { get; set; }
    }
}