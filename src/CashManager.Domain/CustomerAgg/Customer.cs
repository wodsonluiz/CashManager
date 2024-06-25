using System.Collections.Generic;

namespace CashManager.Domain.CustomerAgg
{
    public class Customer
    {
        public string? Id { get; set; }

        public string? Document { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Pass { get; set; }

        //Administrator, Common user
        public string? Profile { get; set; }
        public Company? Company { get; set; }
        public List<BankAccount>? BankAccount { get; set; }
    }
}