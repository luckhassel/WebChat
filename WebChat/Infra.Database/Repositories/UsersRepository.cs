using Domain.Adapters;
using Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Database
{
    public class UsersRepository : IUsersRepositoryAdapter
    {
        private readonly MessagesLibraryContext _context;

        public UsersRepository(MessagesLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public User GetUser(string userName)
        {
            return _context.Users.Where(u => u.Username == userName).FirstOrDefault();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool UserExists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }
    }
}
