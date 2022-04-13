using Application.Services.Auth;
using AutoMapper;
using Domain.Adapters;
using Domain.Entities;
using Domain.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebChat.Controllers;
using Xunit;

namespace UnitTest
{
    public class UserRepoServiceTests
    {

        [Fact]
        public void AddUserSuccess()
        {
            User user = new User { Username = "name", Password = "password" };
            var repo = A.Fake<IUsersRepositoryAdapter>();
            var userRepo = new UserRepositoryService(repo);

            A.CallTo(() => repo.AddUser(user));
            A.CallTo(() => repo.UserExists(user.Username)).Returns(false);
            var returnValue =  userRepo.AddUser(user);

            Assert.True(returnValue);
        }

        [Fact]
        public void AddNullUserFail()
        {
            User user = null;
            var repo = A.Fake<IUsersRepositoryAdapter>();
            var userRepo = new UserRepositoryService(repo);

            var returnValue = userRepo.AddUser(user);

            Assert.False(returnValue);
        }

        [Fact]
        public void AddUserExistsFail()
        {
            User user = new User { Username = "name", Password = "password" };
            var repo = A.Fake<IUsersRepositoryAdapter>();
            var userRepo = new UserRepositoryService(repo);

            A.CallTo(() => repo.UserExists(user.Username)).Returns(true);
            var returnValue = userRepo.AddUser(user);

            Assert.False(returnValue);
        }

        [Fact]
        public void GetUserSuccess()
        {
            User user = new User { Username = "name", Password = "password" };
            var repo = A.Fake<IUsersRepositoryAdapter>();
            var userRepo = new UserRepositoryService(repo);

            A.CallTo(() => repo.GetUser(user.Username)).Returns(user);
            var returnValue = userRepo.GetUser(user.Username);

            Assert.Equal(user, returnValue);
        }

        [Fact]
        public void GetNullUserFail()
        {
            var repo = A.Fake<IUsersRepositoryAdapter>();
            var userRepo = new UserRepositoryService(repo);

            var returnValue = userRepo.GetUser(null);

            Assert.Null(returnValue);
        }
    }
}
