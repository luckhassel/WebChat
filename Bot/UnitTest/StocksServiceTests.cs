using Application.Services;
using Domain.Adapters;
using FakeItEasy;
using Xunit;

namespace UnitTest
{
    public class StocksServiceTests
    {

        [Fact]
        public async void GetStocksSuccess()
        {
            var adapter = A.Fake<IStockAdapter>();
            var stocks = new StocksService(adapter);

            A.CallTo(() => adapter.GetStocks("", "")).Returns("");
            var stocksResult = await stocks.GetStocks("", "");

            Assert.Equal("", stocksResult);
        }
    }
}
