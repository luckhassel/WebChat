namespace Bot.Settings
{
    public interface IBrokerConfiguration
    {
        public string StoqUrl { get; }
        public RabbitMqConfiguration RabbitConfig { get; }
    }
}
