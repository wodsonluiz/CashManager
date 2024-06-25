using CashManager.Daily.Api.Domain;
using CashManager.Daily.Api.Domain.CustomerAgg;
using CashManager.Daily.Api.Infrastructure.Mongo;
using CashManager.Daily.Api.Infrastructure.RabbitMq;
using CashManager.Daily.Api.Repository;
using CashManager.Daily.Api.Repository.Customer;
using CashManager.Daily.Api.Services.Abstractions;
using CashManager.Daily.Api.Services.Implementations;
using EasyNetQ;
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

            services.AddScoped<IRepository<Transaction>>(sp =>
            {
                var provider = sp.GetRequiredService<IMongoProvider>();

                return new Repository<Transaction>(provider, "Transactions");
            });

            return services;
        }

        public static IServiceCollection AddServiceAppServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerAppService, CustomerAppServices>();
            services.AddScoped<ITransactionAppService, TransactionAppService>();

            return services;
        }

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("RabbitMq");
            var options = new ProducerOptions();
            configuration.GetSection(ProducerOptions.PREFIX).Bind(options);

            var bus = RabbitHutch.CreateBus(conn);

            services.AddSingleton(options);
            services.AddSingleton(bus);
            services.AddSingleton<IProducerMessageHandler, ProducerMessageHandler>();

            return services;
        }
    }
}