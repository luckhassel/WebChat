using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
