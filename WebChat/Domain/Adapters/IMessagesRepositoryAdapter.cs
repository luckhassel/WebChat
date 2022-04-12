using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Adapters
{
    public interface IMessagesRepositoryAdapter
    {
        public Task<IEnumerable<Message>> GetFirstMessages(string room, int amount);
        public void AddMessage(Message message);
        public Task Save();
    }
}
