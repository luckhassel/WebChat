using Domain.Adapters.Entities;
using Domain.Services;
using System;

namespace Application.Services
{
    public class MessagesService : IMessagesService
    {
        public string ConvertToMessage(StockToReceiveDTO stockModel)
        {
            return $"{stockModel.Symbol} quote is ${stockModel.Close} per share.";
        }

        public StockToReceiveDTO ConvertToStock(string response)
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
