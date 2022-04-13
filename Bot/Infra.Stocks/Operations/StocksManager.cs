using Domain.Adapters;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infra.Stocks.Operations
{
    public class StocksManager : IStockAdapter
    {
        public async Task<string> GetStocks(string url, string code)
        {
            {
                HttpClient client = new HttpClient();
                string urlFormatted = string.Format(url, code);
                HttpResponseMessage response = await client.GetAsync(urlFormatted);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return "Not able to get stock";
            }
        }
    }
}
