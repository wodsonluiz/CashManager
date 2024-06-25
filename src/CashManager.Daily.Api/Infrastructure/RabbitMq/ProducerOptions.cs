namespace CashManager.Daily.Api.Infrastructure.RabbitMq
{
    public class ProducerOptions
    {
        public const string PREFIX = "RabbitMqSettings";

        public string? QueueName { get; set; }
        public string? ExchangeName { get; set; }
        public string? ExchangeType { get; set; }
        public string? RoutingKey { get; set; }
    }
}