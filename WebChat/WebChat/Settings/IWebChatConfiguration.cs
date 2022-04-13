namespace WebChat.Settings
{
    public interface IWebChatConfiguration
    {
        public string BotUrl { get; }
        public RabbitMqConfiguration RabbitConfig { get; }
    }
}
