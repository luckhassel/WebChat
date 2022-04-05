using WebChat.Entities;

namespace WebChat.Services.Users
{
    public interface IUsersRepository
    {
        public void AddUser(User user);
        public User GetUser(string userName);
        public bool PasswordMatch(string password, string passwordStored);
        public bool UserExists(string username);
        public void Save();
    }
}
