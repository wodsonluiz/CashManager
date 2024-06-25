using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;

namespace CashManager.Daily.Api.Infrastructure.RabbitMq
{
    public interface IMessageHandler
    {
        Task CreateMessageInBroker<T>(T entity);
    }

    public class MessageHandler: IMessageHandler
    {
        private readonly IBus _bus;

        public MessageHandler(IBus bus)
        {
            _bus = bus;
        }

        public Task CreateMessageInBroker<T>(T entity)
        {
            var message = new Message<T>(entity);

            return _bus.PubSub.PublishAsync(message);
        }
    }
}