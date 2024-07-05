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
using Serilog;

namespace CashManager.Daily.Api.Services.Implementations
{
    public class CustomerAppServices: ICustomerAppService
    {
        private readonly IRepository<CustomerTransaction> _repository;
        private readonly IProducerMessageHandler _producerMessageHandler;
        private readonly ILogger _logger;

        public CustomerAppServices(IRepository<CustomerTransaction> repository, 
            IProducerMessageHandler producerMessageHandler, ILogger logger)
        {
            _repository = repository;
            _producerMessageHandler = producerMessageHandler;
            _logger = logger;
        }

        public async Task CreateCustomerAsync(CustomerTransactionRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var customer = request.MapToCustomerTransaction();
                await _repository.AddAsync(customer, cancellationToken);
                await _producerMessageHandler.CreateMessageInBrokerAsync(customer, cancellationToken);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, $"Erro in {nameof(CreateCustomerAsync)}");
            }
        }

        public async Task<IEnumerable<CustomerTransactionRequest>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var customersRequest = new List<CustomerTransactionRequest>();

            try
            {
                var customers = await _repository.GetAllAsync(cancellationToken);

                if(customers.Any())
                {
                    foreach (var customer in customers)
                    {
                        customersRequest.Add(customer.MapToCustomerTransactionRequest());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Erro in {nameof(GetAllAsync)}");
            }

            return customersRequest;
        }

        public async Task<CustomerTransactionRequest> GetByDocumentAsync(string document, CancellationToken cancellationToken = default)
        {
            CustomerTransactionRequest customer = null;

            try
            {
                Expression<Func<CustomerTransaction, bool>> filter = customer => customer.Document == document;

                var customers = await _repository.GetByFilterAsync(filter, cancellationToken);

                customer = customers?.FirstOrDefault()!.MapToCustomerTransactionRequest();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Erro in {nameof(GetByDocumentAsync)}");
            }

            return customer;
        }

        public async Task<CustomerTransactionRequest> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            CustomerTransactionRequest customer = null;

            try
            {
                var customerResult = await _repository.GetByIdAsync(id, cancellationToken);

                customer = customerResult?.MapToCustomerTransactionRequest()!;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Erro in {nameof(GetByIdAsync)}");
            }

            return customer;
        }
    }
}