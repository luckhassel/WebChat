using System.Net.Http;
using System.Threading.Tasks;

namespace WebChat.Services.Stocks
{
    public class Stocks : IStocks
    {
        public async Task<string> GetStocks(string code)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://localhost:44331/api/stocks/{code}");

            return await response.Content.ReadAsStringAsync();
        }

        public string GetStockCode(string message)
        {
            return message.Split('=')[1];
        }
    }
}
