using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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

        public async Task CreateCustomerAsync(CustomerTransactionRequest request, CancellationToken cancellationToken = default)
        {
            var customer = request.MapToCustomerTransaction();
            await _repository.AddAsync(customer, cancellationToken);
            await _producerMessageHandler.CreateMessageInBrokerAsync(customer, cancellationToken);
        }

        public async Task<IEnumerable<CustomerTransactionRequest>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var customers = await _repository.GetAllAsync(cancellationToken);
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

        public async Task<CustomerTransactionRequest> GetByDocumentAsync(string document, CancellationToken cancellationToken = default)
        {
            Expression<Func<CustomerTransaction, bool>> filter = customer => customer.Document == document;

            var customers = await _repository.GetByFilterAsync(filter, cancellationToken);

            if(!customers.Any())
                return null;

            return customers!.FirstOrDefault()!.MapToCustomerTransactionRequest();
        }

        public async Task<CustomerTransactionRequest> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var customer = await _repository.GetByIdAsync(id, cancellationToken);

            return customer?.MapToCustomerTransactionRequest()!;
        }
    }
}