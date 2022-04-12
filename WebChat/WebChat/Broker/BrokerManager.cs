using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;
using WebChat.Settings;

namespace WebChat.Broker
{
    public class BrokerManager : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private Message _message;
        private readonly IBrokerService _broker;
        private readonly IWebChatConfiguration _configuration;

        public BrokerManager(IBrokerService broker, IWebChatConfiguration configuration)
        {
            _configuration = configuration;
            _broker = broker;
            var factory = new ConnectionFactory
            {
                HostName = _configuration.RabbitConfig.Host
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                    queue: _configuration.RabbitConfig.Queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                _message = JsonConvert.DeserializeObject<Message>(contentString);
                _broker.AddMessage(_message);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(_configuration.RabbitConfig.Queue, false, consumer);
            return Task.CompletedTask;
        }
    }
}
