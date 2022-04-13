using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IMessagesRepositoryService
    {
        public bool AddMessage(Message message);

        public Task<IEnumerable<Message>> GetFirstMessages(string room, int amount);

        public Task Save();
    }
}
