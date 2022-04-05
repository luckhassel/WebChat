using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Message> GetFirstMessages()
        {
            return _context.Messages.OrderBy(m => m.Date).Take(50);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
