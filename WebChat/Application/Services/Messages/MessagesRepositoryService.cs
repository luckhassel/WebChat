using Domain.Adapters;
using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MessagesRepositoryService:IMessagesRepositoryService
    {
        private readonly IMessagesRepositoryAdapter _repo;
        public MessagesRepositoryService(IMessagesRepositoryAdapter repo)
        {
            _repo = repo;
        }

        public bool AddMessage(Message message)
        {
            if(message != null)
            {
                _repo.AddMessage(message);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Message>> GetFirstMessages(string room, int amount)
        {
            if(room != null) 
            {
                return await _repo.GetFirstMessages(room, amount);
            }
            return null;
        }

        public async Task Save()
        {
            await _repo.Save();
        }
    }
}
