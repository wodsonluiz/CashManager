using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;

namespace CashManager.Infrastructure.RabbitMq
{
    public class ProducerMessageHandler: IProducerMessageHandler
    {
        private readonly IBus _bus;
        private readonly RabbitMqOptions _options;
        private readonly Exchange _exchange;

        public ProducerMessageHandler(IBus bus, RabbitMqOptions options)
        {   
            _exchange = bus.Advanced.ExchangeDeclare(options.ExchangeName, ExchangeType.Direct);
            var queue = bus.Advanced.QueueDeclare(options.QueueName);

            bus.Advanced.Bind(_exchange, queue, options.RoutingKey);

            _options = options;
            _bus = bus;
        }

        public Task CreateMessageInBrokerAsync<T>(T entity, CancellationToken cancellationToken = default)
        {
            var message = new Message<T>(entity);

            return _bus.Advanced.PublishAsync(message: message, 
                exchange: _exchange, 
                routingKey: _options.RoutingKey, 
                mandatory: false,
                cancellationToken: cancellationToken);
        }
    }
}