using System;
using System.Threading;
using System.Threading.Tasks;
using CashManager.Domain.CustomerTransactionAgg;
using CashManager.Domain.ReportAgg;
using CashManager.Infrastructure.RabbitMq;
using CashManager.Infrastructure.Repository;
using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CashManager.Report.Api
{
    public class Worker : BackgroundService
    {
        private readonly IBus _bus;
        private readonly IRepository<ReportDaily> _repository;
        private readonly ILogger _logger;
        private readonly Queue _queue;

        public Worker(IBus bus, RabbitMqOptions options, IRepository<ReportDaily> repository, ILogger logger)
        {
            _bus = bus;
            _repository = repository;
            _logger = logger;
            _queue = bus.Advanced.QueueDeclare(options.QueueName);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Worker started");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _bus.Dispose();
            _logger.Information("Worker stopped");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.Advanced.Consume<CustomerTransaction>(_queue, (message, info) => 
                HandlerMessage(message.Body, stoppingToken));

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    
        private Task HandlerMessage(CustomerTransaction customerTransaction, CancellationToken cancellationToken = default)
        {
            if(customerTransaction == null)
                return Task.CompletedTask;

            try
            {
                var reportDaily = new ReportDaily(id: null,
                    name: customerTransaction.Name, 
                    document: customerTransaction.Document, 
                    transaction: customerTransaction.Transaction);

                _logger.Information("Message Processed - Document customer: {0}", customerTransaction.Document);

                return _repository.AddAsync(reportDaily, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Erro in consumer message ");
                return Task.CompletedTask;
            }
            
        }
    }
}