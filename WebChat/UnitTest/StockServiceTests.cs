using Application.Services.Auth;
using Application.Services.Stocks;
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
    public class StockServiceTests
    {

        [Fact]
        public void GetStockCodeSuccess()
        {
            string message = "test";
            var adapter = A.Fake<IStocksAdapter>();
            var stock = new StockService(adapter);

            A.CallTo(() => adapter.GetStockCode(message)).Returns(message);
            var returnValue =  stock.GetStockCode(message);

            Assert.Equal(message, returnValue);
        }
    }
}
