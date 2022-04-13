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
    public class BrokerServiceTests
    {

        [Fact]
        public void GetMessageSuccess()
        {
            Message message = new Message { Content = "test" };
            
            var broker = new BrokerService();
            broker.AddMessage(message);
            var returnMessage = broker.GetMessage();

            Assert.Equal(message, returnMessage);
        }
    }
}
