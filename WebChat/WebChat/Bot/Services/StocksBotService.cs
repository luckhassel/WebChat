using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebChat.Bot.Models;

namespace WebChat.Bot.Services
{
    public class StocksBotService : IStocksBotService
    {
        public string ConvertToMessage(StockToReceiveDTO stockModel)
        {
            return $"{stockModel.Symbol} quote is ${stockModel.Close} per share.";
        }

        public async Task<StockToReceiveDTO> GetStocks(string code)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://stooq.com/q/l/?s={code}&f=sd2t2ohlcv&h&e=csv,");

            if (response.IsSuccessStatusCode)
            {
                return ConvertToStock(await response.Content.ReadAsStringAsync());
            }

            throw new Exception("Not able to get stock");
        }

        private StockToReceiveDTO ConvertToStock(string response)
        {
            var stockDataArray = response.Split('\n')[1].Split(',');

            try
            {
                var stockData = new StockToReceiveDTO()
                {
                    Symbol = stockDataArray[0],
                    Date = Convert.ToDateTime(stockDataArray[1]),
                    Time = Convert.ToDateTime(stockDataArray[2]),
                    Open = Convert.ToDouble(stockDataArray[3]),
                    High = Convert.ToDouble(stockDataArray[4]),
                    Low = Convert.ToDouble(stockDataArray[5]),
                    Close = Convert.ToDouble(stockDataArray[6]),
                    Volume = Convert.ToDouble(stockDataArray[7])
                };
                return stockData;
            }
            catch
            {
                throw;
            }
        }
    }
}
