using System.Threading;
using System.Threading.Tasks;

namespace CashManager.Infrastructure.RabbitMq
{
    public interface IProducerMessageHandler
    {
        Task CreateMessageInBrokerAsync<T>(T entity, CancellationToken cancellationToken = default);
    }
}