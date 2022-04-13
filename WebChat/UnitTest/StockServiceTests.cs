using Application.Services.Stocks;
using Domain.Adapters;
using FakeItEasy;
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
            var returnValue = stock.GetStockCode(message);

            Assert.Equal(message, returnValue);
        }
    }
}
