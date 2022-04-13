using Domain.Adapters;
using Domain.Services;
using System;
using System.Threading.Tasks;

namespace Application.Services.Stocks
{
    public class StockService : IStockService
    {
        private readonly IStocksAdapter _adapter;
        public StockService(IStocksAdapter adapter)
        {
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }
        public string GetStockCode(string message)
        {
            return _adapter.GetStockCode(message);
        }

        public async Task GetStocks(string url, string code)
        {
            await _adapter.GetStocks(url, code);
        }
    }
}
