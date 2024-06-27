using System;
using CashManager.Daily.Api.Models;
using CashManager.Domain.CustomerTransactionAgg;
using CashManager.Domain.ObjectValue;

namespace CashManager.Daily.Api.Shared
{
    public static class CustomerMapperExtensions
    {
        public static CustomerTransaction MapToCustomerTransaction(this CustomerTransactionRequest request)
        {
            var company = new Company(request?.Company?.Name);
            var transaction = new Transaction(request.Transaction.Amount, DateTime.UtcNow, request.Transaction.OperationType);
            
            return new CustomerTransaction(request?.Id, 
                request?.Document, 
                request?.Name, 
                request?.Email, 
                request?.Profile, 
                company, 
                transaction);
        }

        public static CustomerTransactionRequest MapToCustomerTransactionRequest(this CustomerTransaction customer)
        {
            var request = new CustomerTransactionRequest
            {
                Id = customer.Id,
                Name = customer.Name,
                Profile = customer.Profile,
                Email = customer.Email,
                Document = customer.Document,
                Company = new CompanyRequest
                {
                    Name = customer.Name
                },
                Transaction = new TransactionRequest
                {
                    Amount = customer.Transaction.Amount,
                    OperationType = customer.Transaction.OperationType,
                    DateOperation = customer.Transaction.DateOperation
                }
            };

            return request;
        }
    }
}