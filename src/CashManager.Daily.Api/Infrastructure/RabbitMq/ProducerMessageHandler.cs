using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;

namespace CashManager.Daily.Api.Infrastructure.RabbitMq
{
    public class ProducerMessageHandler: IProducerMessageHandler
    {
        private readonly IBus _bus;
        private readonly Exchange _exchange;

        public ProducerMessageHandler(IBus bus, ProducerOptions options)
        {   
            _exchange = new Exchange(options.ExchangeName, options.ExchangeType, true, false);
            _bus = bus;
        }

        public Task CreateMessageInBroker<T>(T entity)
        {
            var message = new Message<T>(entity);

            return _bus.Advanced.PublishAsync(_exchange, "transactions.#", false, message);
        }
    }
}