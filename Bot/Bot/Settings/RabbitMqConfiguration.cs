
namespace Bot.Settings
{
    public class RabbitMqConfiguration
    {
        public RabbitMqConfiguration(string host, string queue)
        {
            Host = host;
            Queue = queue;
        }
        public string Host { get; set; }
        public string Queue { get; set; }
    }
}
