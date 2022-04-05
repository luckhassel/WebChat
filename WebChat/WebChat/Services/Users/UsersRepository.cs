using System;
using System.Linq;
using WebChat.DbContexts;
using WebChat.Entities;
using WebChat.Services.Auth;

namespace WebChat.Services.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MessagesLibraryContext _context;
        private readonly IPassword _password;

        public UsersRepository(MessagesLibraryContext context, IPassword password)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.Password = _password.HashPwd(user.Password);
            _context.Users.Add(user);
        }

        public User GetUser(string userName)
        {
            if (userName == null)
                throw new ArgumentNullException(nameof(userName));
            return _context.Users.Where(u => u.Username == userName).FirstOrDefault();
        }

        public bool PasswordMatch(string password, string passwordStored)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            if (passwordStored == null)
                throw new ArgumentNullException(nameof(passwordStored));

            return _password.VerifyPwd(password, passwordStored);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool UserExists(string username)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));

            return _context.Users.Any(u => u.Username == username);
        }
    }
}
