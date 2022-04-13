using Application.Services;
using Domain.Adapters.Entities;
using System;
using Xunit;

namespace UnitTest
{
    public class MessageServiceTests
    {

        [Fact]
        public void ConvertToMessageSuccess()
        {
            DateTime date = DateTime.Now;
            double number = 10;
            StockToReceiveDTO stock = new StockToReceiveDTO()
            {
                Date = date,
                Close = number,
                High = number,
                Low = number,
                Open = number,
                Symbol = "",
                Time = date,
                Volume = number
            };
            var pattern = $"{stock.Symbol} quote is ${stock.Close} per share.";

            var message = new MessagesService();
            var messageConverted = message.ConvertToMessage(stock);

            Assert.Equal(pattern, messageConverted);
        }
    }
}
