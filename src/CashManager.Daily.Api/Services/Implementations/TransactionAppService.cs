using System.Threading.Tasks;
using CashManager.Daily.Api.Domain;
using CashManager.Daily.Api.Infrastructure.RabbitMq;
using CashManager.Daily.Api.Repository;
using CashManager.Daily.Api.Services.Abstractions;

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
            await _producerMessageHandler.CreateMessageInBroker(transaction);
            await _repository.Add(transaction);
        }

        public async Task CreateCredit(Transaction transaction)
        {
            await _producerMessageHandler.CreateMessageInBroker(transaction);
            await _repository.Add(transaction);
        }
    }
}