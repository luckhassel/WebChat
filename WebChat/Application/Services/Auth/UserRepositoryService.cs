using Domain.Adapters;
using Domain.Entities;
using Domain.Services;
using System;
using System.Threading.Tasks;

namespace Application.Services.Auth
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private readonly IUsersRepositoryAdapter _repo;
        public UserRepositoryService(IUsersRepositoryAdapter repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        public bool AddUser(User user)
        {
            if (user != null && !UserExists(user.Username))
            {
                user.Password = HashPassword(user.Password);
                _repo.AddUser(user);
                return true;
            }
            return false;
        }

        public User GetUser(string userName)
        {
            if (userName == null)
            {
                return null;
            }
            return _repo.GetUser(userName);
        }

        public async Task Save()
        {
            await _repo.Save();
        }

        public bool PasswordMatch(string password, string passwordStored)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordStored);
        }

        private bool UserExists(string username)
        {
            if (username == null)
            {
                return false;
            }
            return _repo.UserExists(username);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
