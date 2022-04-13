using Domain.Adapters;
using Domain.Services;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StocksService : IStocksService
    {
        private readonly IStockAdapter _stocks;
        public StocksService(IStockAdapter stocks)
        {
            _stocks = stocks ?? throw new ArgumentNullException(nameof(stocks));
        }

        public async Task<string> GetStocks(string url, string code)
        {
            return await _stocks.GetStocks(url, code);
        }
    }
}
