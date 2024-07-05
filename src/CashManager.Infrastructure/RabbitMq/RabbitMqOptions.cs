namespace CashManager.Infrastructure.RabbitMq
{
    public class RabbitMqOptions
    {
        public const string PREFIX = "RabbitMqSettings";

        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public string RoutingKey { get; set; }
    }
}