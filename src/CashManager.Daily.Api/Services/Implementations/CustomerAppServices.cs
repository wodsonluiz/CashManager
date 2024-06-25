using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CashManager.Daily.Api.Domain.CustomerAgg;
using CashManager.Daily.Api.Infrastructure.RabbitMq;
using CashManager.Daily.Api.Repository;
using CashManager.Daily.Api.Services.Abstractions;

namespace CashManager.Daily.Api.Services.Implementations
{
    public class CustomerAppServices: ICustomerAppService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IMessageHandler _messageHandler;

        public CustomerAppServices(IRepository<Customer> repository, IMessageHandler messageHandler)
        {
            _repository = repository;
            _messageHandler = messageHandler;
        }

        public Task CreateCustomer(Customer customer)
        {
            _messageHandler.CreateMessageInBroker<Customer>(customer);
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