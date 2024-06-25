using System;
using System.Collections.Generic;
using System.Linq;
using CashManager.Daily.Api.Models;
using CashManager.Domain.CustomerTransactionAgg;

namespace CashManager.Daily.Api.Shared
{
    public static class CustomerMapperExtensions
    {
        public static CustomerTransaction MapToCustomerTransaction(this CustomerTransactionRequest request)
        {
            var company = new Company(request?.Company?.Name);
            var transaction = new Transaction(request.Transaction.Ammout, DateTime.UtcNow, request.Transaction.OperationType);
            
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
                Company = new CompanyRequest
                {
                    Name = customer.Name
                },
                Transaction = new TransactionRequest
                {
                    Ammout = customer.Transaction.Ammout,
                    OperationType = customer.Transaction.OperationType,
                    DateOperation = customer.Transaction.DateOperation
                }
            };

            return request;
        }
    }
}