using Domain.Adapters;
using Domain.Adapters.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Broker.Operations
{
    public class BrokerManager : IBrokerAdapter
    {
        private ConnectionFactory _factory;
        private string _queueName;
        public void AddToQueue(string host, string queue, string message)
        {
            _factory = new ConnectionFactory { HostName = host };
            _queueName = queue;
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            DeclareQueue(channel);
            
            string stringfiedMessage = JsonConvert.SerializeObject(new StockToSendDTO { User = "StockBot", Content = message });
            byte[] bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

            PublishMessage(channel, bytesMessage);
        }

        private void DeclareQueue(IModel channel)
        {
            channel.QueueDeclare
                (
                    queue: _queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );
        }

        private void PublishMessage(IModel channel, byte[] bytesMessage)
        {
            channel.BasicPublish
                (
                    exchange: "",
                    routingKey: _queueName,
                    basicProperties: null,
                    body: bytesMessage
                );
        }
    }
}
