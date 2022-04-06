using System.Collections.Generic;
using System.Threading.Tasks;
using WebChat.Entities;

namespace WebChat.Services.Messages
{
    public interface IMessagesRepository
    {
        public Task<IEnumerable<Message>> GetFirstMessages(string room, int amount);
        public void AddMessage(Message message);
        public void Save();
    }
}
