using System.Collections.Generic;
using WebChat.Entities;

namespace WebChat.Services.Messages
{
    public interface IMessagesRepository
    {
        public IEnumerable<Message> GetFirstMessages();
        public void AddMessage(Message message);
        public void Save();
    }
}
