using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CashManager.Daily.Api.Models;
using CashManager.Daily.Api.Services.Abstractions;
using CashManager.Daily.Api.Shared;
using CashManager.Domain.CustomerTransactionAgg;
using CashManager.Infrastructure.RabbitMq;
using CashManager.Infrastructure.Repository;

namespace CashManager.Daily.Api.Services.Implementations
{
    public class CustomerAppServices: ICustomerAppService
    {
        private readonly IRepository<CustomerTransaction> _repository;
        private readonly IProducerMessageHandler _producerMessageHandler;

        public CustomerAppServices(IRepository<CustomerTransaction> repository, IProducerMessageHandler producerMessageHandler)
        {
            _repository = repository;
            _producerMessageHandler = producerMessageHandler;
        }

        public async Task CreateCustomer(CustomerTransactionRequest request)
        {
            var customer = request.MapToCustomerTransaction();
            await _repository.Add(customer);
            await _producerMessageHandler.CreateMessageInBroker(customer);
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

            if(!customers.Any())
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