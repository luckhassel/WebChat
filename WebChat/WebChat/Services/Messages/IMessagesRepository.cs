using System.Collections.Generic;
using System.Threading.Tasks;
using WebChat.Entities;

namespace WebChat.Services.Messages
{
    public interface IMessagesRepository
    {
        public Task<IEnumerable<Message>> GetFirstMessages(int amount, string room);
        public void AddMessage(Message message);
        public void Save();
    }
}
