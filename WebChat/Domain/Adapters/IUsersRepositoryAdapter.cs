using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Adapters
{
    public interface IUsersRepositoryAdapter
    {
        public void AddUser(User user);
        public User GetUser(string userName);
        public bool UserExists(string username);
        public Task Save();
    }
}
