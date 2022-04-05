using System.Threading.Tasks;
using WebChat.Bot.Models;

namespace WebChat.Bot.Services
{
    public interface IStocksBotService
    {
        public Task<StockToReceiveDTO> GetStocks(string code);
        public string ConvertToMessage(StockToReceiveDTO stockModel);
    }
}
