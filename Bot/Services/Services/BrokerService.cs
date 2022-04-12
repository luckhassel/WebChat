using Domain.Adapters;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class BrokerService : IBrokerService
    {
        private readonly IBrokerAdapter _broker;
        public BrokerService(IBrokerAdapter broker)
        {
            _broker = broker;
        }
        public void AddToQueue(string host, string queue, string message)
        {
            _broker.AddToQueue(host, queue, message);
        }
    }
}
