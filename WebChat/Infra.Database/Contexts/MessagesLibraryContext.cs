using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database
{
    public class MessagesLibraryContext : DbContext
    {
        public MessagesLibraryContext(DbContextOptions<MessagesLibraryContext> options)
           : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
