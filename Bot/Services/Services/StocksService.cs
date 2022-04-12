using Domain.Adapters;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StocksService : IStocksService
    {
        private readonly IStockAdapter _stocks;
        public StocksService(IStockAdapter stocks)
        {
            _stocks = stocks;
        }

        public async Task<string> GetStocks(string url, string code)
        {
            return await _stocks.GetStocks(url, code);
        }
    }
}
