using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.DbContexts;
using WebChat.Entities;

namespace WebChat.Services.Messages
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly MessagesLibraryContext _context;
        public MessagesRepository(MessagesLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            _context.Add(message);
        }

        public async Task<IEnumerable<Message>> GetFirstMessages(string room, int amount)
        {
            return await _context.Messages.OrderByDescending(m => m.Date).Where(m => m.Room == room).Take(amount).ToListAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
