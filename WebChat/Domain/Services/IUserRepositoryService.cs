using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUserRepositoryService
    {
        public bool AddUser(User user);
        public User GetUser(string userName);
        public Task Save();
        public bool PasswordMatch(string password, string passwordStored);
    }
}
