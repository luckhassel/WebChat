using Application.Services;
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
    public class MessageRepoServiceTests
    {

        [Fact]
        public void AddMessageSuccess()
        {
            Message message = new Message { Content = "test" };
            var repo = A.Fake<IMessagesRepositoryAdapter>();
            var messageRepo = new MessagesRepositoryService(repo);

            A.CallTo(() => repo.AddMessage(message));

            var returnValue =  messageRepo.AddMessage(message);

            Assert.True(returnValue);
        }

        [Fact]
        public void AddNullMessageFail()
        {
            Message message = null;
            var repo = A.Fake<IMessagesRepositoryAdapter>();
            var messageRepo = new MessagesRepositoryService(repo);

            var returnValue = messageRepo.AddMessage(message);

            Assert.False(returnValue);
        }

        [Fact]
        public void GetFirstMessagesSuccess()
        {
            IEnumerable<Message> messages = new Message[1];
            string room = "test";
            int amount = 50;
            var repo = A.Fake<IMessagesRepositoryAdapter>();
            A.CallTo(() => repo.GetFirstMessages(room, amount)).Returns(messages);
            var messageRepo = new MessagesRepositoryService(repo);

            var returnValue = messageRepo.GetFirstMessages(room, amount);

            Assert.Equal(messages, returnValue.Result);
        }

        [Fact]
        public void GetFirstMessagesNullRoomFail()
        {
            string room = null;
            int amount = 50;
            var repo = A.Fake<IMessagesRepositoryAdapter>();
            var messageRepo = new MessagesRepositoryService(repo);

            var returnValue = messageRepo.GetFirstMessages(room, amount);

            Assert.Null(returnValue.Result);
        }

        [Fact]
        public void TrueStockMessageSuccess()
        {
            var messages = new MessagesService();

            var returnValue = messages.IsStockBotMessage("/stock=");

            Assert.True(returnValue);
        }

        [Fact]
        public void FalseStockMessageSuccess()
        {
            var messages = new MessagesService();

            var returnValue = messages.IsStockBotMessage("/stock");

            Assert.False(returnValue);
        }
    }
}
