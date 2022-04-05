using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebChat.Entities;
using WebChat.Models;
using WebChat.Services.Broker;

namespace WebChat.MessageBroker
{
    public class ProcessMesageConsumer : BackgroundService
    {
        private readonly RabbitMqConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private Message _message;
        private readonly IBroker _broker;

        public ProcessMesageConsumer(IOptions<RabbitMqConfiguration> option, IBroker broker)
        {
            _broker = broker;
            _configuration = option.Value;
            var factory = new ConnectionFactory
            {
                HostName = _configuration.Host
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                    queue: _configuration.Queue,
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

            _channel.BasicConsume(_configuration.Queue, false, consumer);
            return Task.CompletedTask;
        }
    }
}
