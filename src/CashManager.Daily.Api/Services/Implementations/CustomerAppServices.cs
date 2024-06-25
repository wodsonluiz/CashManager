using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CashManager.Daily.Api.Services.Abstractions;
using CashManager.Domain.CustomerAgg;
using CashManager.Infrastructure.Repository;

namespace CashManager.Daily.Api.Services.Implementations
{
    public class CustomerAppServices: ICustomerAppService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerAppServices(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public Task CreateCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            return _repository.Add(customer);
        }

        public Task DeleteCustomer(string id)
        {
            return _repository.Delete(id);
        }

        public Task<IEnumerable<Customer>> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<Customer> GetByDocument(string document)
        {
            if(string.IsNullOrWhiteSpace(document))
                throw new ArgumentNullException($"Id: invalid");

            Expression<Func<Customer, bool>> filter = customer => customer.Document == document;

            var customers = await _repository.GetByFilter(filter);

            return customers?.FirstOrDefault();
        }

        public Task<Customer> GetById(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException($"Id: invalid");

            return _repository.GetById(id);
        }
    }
}