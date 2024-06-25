using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CashManager.Daily.Api.Models;
using CashManager.Daily.Api.Services.Abstractions;
using CashManager.Daily.Api.Shared;
using CashManager.Domain.CustomerTransactionAgg;
using CashManager.Infrastructure.Repository;

namespace CashManager.Daily.Api.Services.Implementations
{
    public class CustomerAppServices: ICustomerAppService
    {
        private readonly IRepository<CustomerTransaction> _repository;

        public CustomerAppServices(IRepository<CustomerTransaction> repository) =>
            _repository = repository;

        public Task CreateCustomer(CustomerTransactionRequest request)
        {
            var customer = request.MapToCustomerTransaction();
            return _repository.Add(customer);
        }

        public async Task<IEnumerable<CustomerTransactionRequest>> GetAll()
        {
            var customers = await _repository.GetAll();
            var customersRequest = new List<CustomerTransactionRequest>();

            if(customers.Any())
            {
                foreach (var customer in customers)
                {
                    customersRequest.Add(customer.MapToCustomerTransactionRequest());
                }
            }

            return customersRequest;
        }

        public async Task<CustomerTransactionRequest> GetByDocument(string document)
        {
            Expression<Func<CustomerTransaction, bool>> filter = customer => customer.Document == document;

            var customers = await _repository.GetByFilter(filter);

            if(customers.Any())
                return null;

            return customers!.FirstOrDefault()!.MapToCustomerTransactionRequest();
        }

        public async Task<CustomerTransactionRequest> GetById(string id)
        {
            var customer = await _repository.GetById(id);

            return customer?.MapToCustomerTransactionRequest()!;
        }
    }
}