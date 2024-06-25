using CashManager.Daily.Api.Services.Abstractions;
using CashManager.Daily.Api.Services.Implementations;
using CashManager.Domain.CustomerTransactionAgg;
using CashManager.Infrastructure.Mongo;
using CashManager.Infrastructure.RabbitMq;
using CashManager.Infrastructure.Repository;
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
            services.AddScoped<IRepository<CustomerTransaction>>(sp => 
            {
                var provider = sp.GetRequiredService<IMongoProvider>();

                return new Repository<CustomerTransaction>(provider, collectionName: "CustomerTransactions");
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

            services.AddSingleton(options);
            services.AddSingleton(bus);
            services.AddSingleton<IProducerMessageHandler, ProducerMessageHandler>();

            return services;
        }
    }
}