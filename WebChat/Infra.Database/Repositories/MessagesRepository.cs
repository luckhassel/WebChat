using Domain.Adapters;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Database
{
    public class MessagesRepository : IMessagesRepositoryAdapter
    {
        private readonly MessagesLibraryContext _context;
        public MessagesRepository(MessagesLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddMessage(Message message)
        {
            _context.Add(message);
        }

        public async Task<IEnumerable<Message>> GetFirstMessages(string room, int amount)
        {
            return await _context.Messages.OrderByDescending(m => m.Date).Where(m => m.Room == room).Take(amount).ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
