using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebChat.Controllers;
using WebChat.Entities;
using WebChat.Models;
using WebChat.Services.Messages;
using Xunit;

namespace UnitTest
{
    public class ChatControllerTests
    {
        private readonly Message _message = new Message { User = "Test", Content = "Hello World" };
        private readonly IEnumerable<MessageToSendDTO> _messagesToSend;

        [Fact]
        public void AddMessageSuccess()
        {
            var messageRepo = A.Fake<IMessagesRepository>();
            var mapper = A.Fake<IMapper>();
            A.CallTo(() => messageRepo.AddMessage(_message));
            A.CallTo(() => messageRepo.Save());

            var instance = new Chat(messageRepo, mapper);

            var controller = instance.AddMessage(_message);

            Assert.IsType<OkResult>(controller);
        }

        [Fact]
        public void GetMessagesSuccess()
        {
            var messageRepo = A.Fake<IMessagesRepository>();
            var mapper = A.Fake<IMapper>();
            A.CallTo(() => messageRepo.AddMessage(_message));
            A.CallTo(() => messageRepo.Save());
            A.CallTo(() => mapper.Map<IEnumerable<MessageToSendDTO>>(_message))
                .Returns(_messagesToSend);

            var instance = new Chat(messageRepo, mapper);

            var controller = instance.GetFirstMessages();

            Assert.IsType<OkObjectResult>(controller.Result);
        }
    }
}
