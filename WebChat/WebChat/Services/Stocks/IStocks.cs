using System.Threading.Tasks;

namespace WebChat.Services.Stocks
{
    public interface IStocks
    {
        public Task<string> GetStocks(string code);
        public string GetStockCode(string message);
    }
}
