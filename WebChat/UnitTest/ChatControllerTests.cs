//using AutoMapper;
//using FakeItEasy;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using WebChat.Controllers;
//using WebChat.Entities;
//using WebChat.Models;
//using WebChat.Services.Messages;
//using Xunit;

//namespace UnitTest
//{
//    public class ChatControllerTests
//    {
//        private readonly Message _message = new Message { User = "Test", Content = "Hello World" };
//        private readonly IEnumerable<MessageToSendDTO> _messagesToSend;
//        private readonly IEnumerable<Messages> _messagesReceived;

//        [Fact]
//        public void AddMessageSuccess()
//        {
//            var messageRepo = A.Fake<IMessagesRepository>();
//            var mapper = A.Fake<IMapper>();
//            A.CallTo(() => messageRepo.AddMessage(_message));
//            A.CallTo(() => messageRepo.Save());

//            var instance = new ChatController(messageRepo, mapper);

//            var controller = instance.AddMessage(_message);

//            Assert.IsType<OkResult>(controller);
//        }

//        [Fact]
//        public void GetMessagesSuccess()
//        {
//            int amount = 50;
//            string room = "room";
//            var messageRepo = A.Fake<IMessagesRepository>();
//            var mapper = A.Fake<IMapper>();
//            A.CallTo(() => messageRepo.GetFirstMessages(room, amount));
//            A.CallTo(() => messageRepo.Save());
//            A.CallTo(() => mapper.Map<IEnumerable<MessageToSendDTO>>(_message))
//                .Returns(_messagesToSend);

//            var instance = new ChatController(messageRepo, mapper);

//            var controller = instance.GetFirstMessages(room, amount);

//            Assert.IsType<OkObjectResult>(controller.Result.Result);
//        }
//    }
//}
