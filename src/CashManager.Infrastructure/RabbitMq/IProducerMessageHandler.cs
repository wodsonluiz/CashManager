using System.Threading.Tasks;

namespace CashManager.Infrastructure.RabbitMq
{
    public interface IProducerMessageHandler
    {
        Task CreateMessageInBroker<T>(T entity);
    }
}