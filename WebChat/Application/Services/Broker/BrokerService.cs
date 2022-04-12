using Domain.Services;
using System;
using Domain.Entities;

namespace Application.Services
{
    public class BrokerService : IBrokerService
    {
        private Message _message;
        public void AddMessage(Message message)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message)); ;
        }

        public Message GetMessage()
        {
            return _message;
        }
    }
}
