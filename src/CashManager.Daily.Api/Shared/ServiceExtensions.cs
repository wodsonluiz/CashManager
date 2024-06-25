using System.Threading;
using CashManager.Daily.Api.Domain.CustomerAgg;
using CashManager.Daily.Api.Infrastructure.Mongo;
using CashManager.Daily.Api.Infrastructure.RabbitMq;
using CashManager.Daily.Api.Repository;
using CashManager.Daily.Api.Repository.Customer;
using CashManager.Daily.Api.Services.Abstractions;
using CashManager.Daily.Api.Services.Implementations;
using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Daily.Api.Shared
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("Mongo");
            var options = new MongoOptions();
            
            configuration.GetSection(MongoOptions.PREFIX).Bind(options);

            services.AddSingleton<IMongoProvider>(_ => 
            {
                return new MongoProvider(conn, options);
            });

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Customer>>(sp => 
            {
                var provider = sp.GetRequiredService<IMongoProvider>();

                return new Repository<Customer>(provider, "Customers");
            });

            return services;
        }

        public static IServiceCollection AddServiceAppServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerAppService, CustomerAppServices>();

            return services;
        }

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("RabbitMq");
            var options = new ProducerOptions();
            configuration.GetSection(ProducerOptions.PREFIX).Bind(options);

            var bus = RabbitHutch.CreateBus(conn);

            var exchange = new Exchange(options.ExchangeName, options.ExchangeType, true, false);
            var queue = new Queue(options.QueueName, true, false, false);

            bus.Advanced.ExchangeDeclare(exchange.Name, exchange.Type, exchange.IsDurable, exchange.IsAutoDelete);
            bus.Advanced.QueueDeclare(queue.Name);
            bus.Advanced.Bind(exchange: exchange, queue: queue, options.RoutingKey);

            services.AddSingleton(bus);
            
            services.AddScoped<IMessageHandler, MessageHandler>();

            return services;
        }
    }
}