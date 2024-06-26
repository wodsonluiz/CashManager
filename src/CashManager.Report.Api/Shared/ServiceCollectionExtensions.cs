using CashManager.Infrastructure.RabbitMq;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Report.Api.Shared
{
    public static class ServiceCollectionExtensions
    {
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
    }
}