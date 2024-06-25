using System.Threading.Tasks;

namespace CashManager.Daily.Api.Infrastructure.RabbitMq
{
    public interface IProducerMessageHandler
    {
        Task CreateMessageInBroker<T>(T entity);
    }
}