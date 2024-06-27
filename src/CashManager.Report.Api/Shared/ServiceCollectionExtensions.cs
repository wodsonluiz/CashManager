using CashManager.Domain.ReportAgg;
using CashManager.Infrastructure.Mongo;
using CashManager.Infrastructure.RabbitMq;
using CashManager.Infrastructure.Repository;
using CashManager.Report.Api.Services.Abstractions;
using CashManager.Report.Api.Services.Implementations;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Report.Api.Shared
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

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<IRepository<ReportDaily>>(sp => 
            {
                var provider = sp.GetRequiredService<IMongoProvider>();

                return new Repository<ReportDaily>(provider, collectionName: "ReportDailys");
            });

            return services;
        }

        public static IServiceCollection AddServiceAppServices(this IServiceCollection services)
        {
            services.AddScoped<IReportAppService, ReportAppService>();

            return services;
        }
    }
}