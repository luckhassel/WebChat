using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public interface IUserRepositoryService
    {
        public bool AddUser(User user);
        public User GetUser(string userName);
        public void Save();
        public bool PasswordMatch(string password, string passwordStored);
    }
}
