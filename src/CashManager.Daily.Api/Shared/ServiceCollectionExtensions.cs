using CashManager.Daily.Api.Services.Abstractions;
using CashManager.Daily.Api.Services.Implementations;
using CashManager.Domain.CustomerTransactionAgg;
using CashManager.Infrastructure.Mongo;
using CashManager.Infrastructure.RabbitMq;
using CashManager.Infrastructure.Repository;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CashManager.Daily.Api.Shared
{
    public static class ServiceCollectionExtensions
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
            var options = new RabbitMqOptions();
            configuration.GetSection(RabbitMqOptions.PREFIX).Bind(options);

            var bus = RabbitHutch.CreateBus(conn);

            services.AddSingleton(options);
            services.AddSingleton(bus);
            services.AddSingleton<IProducerMessageHandler, ProducerMessageHandler>();

            return services;
        }

        public static IServiceCollection AddLog(this IServiceCollection services, IConfiguration configuration)
        {
            var project = configuration.GetSection("LogSeq").GetSection("Project").Value;
            var url = configuration.GetSection("LogSeq").GetSection("Host").Value;
            var env = configuration.GetSection("LogSeq").GetSection("Env").Value;

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Project", project)
                .Enrich.WithProperty("Environment", env)
                .Enrich.FromLogContext()
                .WriteTo.Seq(url!, Serilog.Events.LogEventLevel.Information)
                .CreateLogger();

            services.AddSingleton(Log.Logger);

            return services;
        }
    }
}