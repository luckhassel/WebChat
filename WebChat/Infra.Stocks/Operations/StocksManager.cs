using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Adapters;

namespace Infra.Stocks.Operations
{
    public class StocksManager : IStocksAdapter
    {
        public async Task GetStocks(string url, string code)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{url}/{code}");

            await response.Content.ReadAsStringAsync();
        }

        public string GetStockCode(string message)
        {
            return message.Split('=')[1];
        }
    }
}
