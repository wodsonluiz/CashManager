using System.Threading;
using System.Threading.Tasks;
using CashManager.Domain.CustomerTransactionAgg;
using CashManager.Domain.ObjectValue;
using CashManager.Domain.ReportAgg;
using CashManager.Infrastructure.RabbitMq;
using CashManager.Infrastructure.Repository;
using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.Hosting;

namespace CashManager.Report.Api
{
    public class Worker : BackgroundService
    {
        private readonly IBus _bus;
        private readonly IRepository<ReportDaily> _repository;
        private readonly Queue _queue;

        public Worker(IBus bus, RabbitMqOptions options, IRepository<ReportDaily> repository)
        {
            _bus = bus;
            _repository = repository;
            _queue = bus.Advanced.QueueDeclare(options.QueueName);
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
            _bus.Advanced.Consume<CustomerTransaction>(_queue, (message, info) => 
            {
                var customerBalance = message.Body;

                if(customerBalance == null)
                    return Task.CompletedTask;

                var reportDaily = new ReportDaily(id: null,
                    name: customerBalance.Name, 
                    document: customerBalance.Document, 
                    transaction: customerBalance.Transaction);

                return _repository.Add(reportDaily);
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}