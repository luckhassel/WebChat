using Domain.Entities;
using Domain.Services;
using System;

namespace Application.Services
{
    public class BrokerService : IBrokerService
    {
        private Message _message;
        public void AddMessage(Message message)
        {
            _message = message;
        }

        public Message GetMessage()
        {
            return _message;
        }
    }
}
