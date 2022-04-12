using System.Threading.Tasks;

namespace Domain.Adapters
{
    public interface IStocksAdapter
    {
        public Task GetStocks(string url, string code);
        public string GetStockCode(string message);
    }
}
