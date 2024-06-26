using System.Threading;
using System.Threading.Tasks;
using CashManager.Domain.CustomerTransactionAgg;
using CashManager.Infrastructure.RabbitMq;
using EasyNetQ;
using Microsoft.Extensions.Hosting;

namespace CashManager.Report.Api
{
    public class Worker : BackgroundService
    {
        private readonly IBus _bus;
        private readonly RabbitMqOptions _options;

        public Worker(IBus bus, RabbitMqOptions options)
        {
            _bus = bus;
            _options = options;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _bus.Dispose();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queue = _bus.Advanced.QueueDeclare(_options.QueueName, cancellationToken: stoppingToken);

            _bus.Advanced.Consume<CustomerTransaction>(queue, (message, info) => 
            {
                return Task.CompletedTask;
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}