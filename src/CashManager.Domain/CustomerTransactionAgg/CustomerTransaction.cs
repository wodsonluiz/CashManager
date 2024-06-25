using System;

namespace CashManager.Domain.CustomerTransactionAgg
{
    public class CustomerTransaction
    {
        public string Id { get; }
        public string Document { get; }
        public string Name { get; }
        public string Email { get; }
        public string Profile { get; }
        public Company Company { get; }
        public Transaction Transaction { get; }

        public CustomerTransaction(string? id, 
            string? document, 
            string? name, 
            string? email, 
            string? profile,
            Company company, 
            Transaction transaction)
        {
            InvalidThrowCustomer(document, name, email, profile, company);

            Id = id ?? Guid.NewGuid().ToString();

            Document = document!;
            Name = name!;
            Email = email!;
            Profile = profile!;
            Company = company;
            Transaction = transaction;
        }

        private void InvalidThrowCustomer(string? document, string? name, string? email, string? profile, Company company)
        {
            if(string.IsNullOrEmpty(document))
                throw new ArgumentException($"invalid field {nameof(document)} ");

            if(string.IsNullOrEmpty(name))
                throw new ArgumentException($"invalid field {nameof(name)} ");

            if(string.IsNullOrEmpty(email))
                throw new ArgumentException($"invalid field {nameof(email)} ");

            if(string.IsNullOrEmpty(profile))
                throw new ArgumentException($"invalid field {nameof(profile)} ");

            if(company == null || string.IsNullOrEmpty(company.Name))
                throw new ArgumentException($"invalid field {nameof(company)}");
        }
    }
}