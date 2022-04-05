using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebChat.Controllers;
using WebChat.Entities;
using WebChat.Services.Users;
using Xunit;

namespace UnitTest
{
    public class LoginControllerTests
    {
        private readonly string _username = "Test";
        private readonly User _user = new User { Username = "Test", Password = "Pwd", Id = 1, Role = "User" };

        [Fact]
        public void AddUserSuccess()
        {
            var userRepo = A.Fake<IUsersRepository>();
            A.CallTo(() => userRepo.UserExists(_username)).Returns(false);
            A.CallTo(() => userRepo.AddUser(_user));
            A.CallTo(() => userRepo.Save());

            var instance = new LoginController(userRepo);

            var controller = instance.AddUser(_user);

            Assert.IsType<CreatedResult>(controller);
        }

        [Fact]
        public void AddUserFailed()
        {
            var userRepo = A.Fake<IUsersRepository>();
            A.CallTo(() => userRepo.UserExists(_username)).Returns(true);
            A.CallTo(() => userRepo.AddUser(_user));
            A.CallTo(() => userRepo.Save());

            var instance = new LoginController(userRepo);

            var controller = instance.AddUser(_user);

            Assert.IsType<BadRequestObjectResult>(controller);
        }

        [Fact]
        public void CheckAddUserFailedMessage()
        {
            string returnedMessage = $"User {_username} already exists";

            var userRepo = A.Fake<IUsersRepository>();
            A.CallTo(() => userRepo.UserExists(_username)).Returns(true);
            A.CallTo(() => userRepo.AddUser(_user));
            A.CallTo(() => userRepo.Save());

            var instance = new LoginController(userRepo);

            var controller = instance.AddUser(_user);

            var contentResult = Assert.IsType<BadRequestObjectResult>(controller);
            Assert.Equal(returnedMessage, contentResult.Value);
        }

        [Fact]
        public void AuthenticationUsernameFailed()
        {
            var userRepo = A.Fake<IUsersRepository>();
            A.CallTo(() => userRepo.GetUser(_username)).Returns(null);

            var instance = new LoginController(userRepo);

            var controller = instance.Authenticate(_user);

            var contentResult = Assert.IsType<NotFoundObjectResult>(controller.Result.Result);
            Assert.Equal(new { message = "Wrong user" }.ToString(), contentResult.Value.ToString());
        }

        [Fact]
        public void AuthenticationPasswordFailed()
        {
            var userRepo = A.Fake<IUsersRepository>();
            A.CallTo(() => userRepo.GetUser(_username)).Returns(_user);
            A.CallTo(() => userRepo.PasswordMatch(_user.Password, _user.Password)).Returns(false);

            var instance = new LoginController(userRepo);

            var controller = instance.Authenticate(_user);

            var contentResult = Assert.IsType<NotFoundObjectResult>(controller.Result.Result);
            Assert.Equal(new { message = "Wrong password" }.ToString(), contentResult.Value.ToString());
        }
    }
}
