using System;
using WebChat.Entities;

namespace WebChat.Services.Broker
{
    public class Broker : IBroker
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
