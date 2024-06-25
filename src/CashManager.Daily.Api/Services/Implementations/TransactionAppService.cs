using System;
using System.Threading.Tasks;
using CashManager.Daily.Api.Services.Abstractions;
using CashManager.Domain.TransactionAgg;
using CashManager.Infrastructure.RabbitMq;
using CashManager.Infrastructure.Repository;

namespace CashManager.Daily.Api.Services.Implementations
{
    public class TransactionAppService: ITransactionAppService
    {
        private readonly IRepository<Transaction> _repository;
        private readonly IProducerMessageHandler _producerMessageHandler;

        public TransactionAppService(IRepository<Transaction> repository, IProducerMessageHandler producerMessageHandler)
        {
            _repository = repository;
            _producerMessageHandler = producerMessageHandler;
        }

        public async Task CreateDebit(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid().ToString();
            await _producerMessageHandler.CreateMessageInBroker(transaction);
            await _repository.Add(transaction);
        }

        public async Task CreateCredit(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid().ToString();
            await _producerMessageHandler.CreateMessageInBroker(transaction);
            await _repository.Add(transaction);
        }
    }
}