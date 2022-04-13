using Domain.Adapters;
using Domain.Services;
using System;

namespace Application.Services
{
    public class BrokerService : IBrokerService
    {
        private readonly IBrokerAdapter _broker;
        public BrokerService(IBrokerAdapter broker)
        {
            _broker = broker ?? throw new ArgumentNullException(nameof(broker));
        }
        public void AddToQueue(string host, string queue, string message)
        {
            _broker.AddToQueue(host, queue, message);
        }
    }
}
