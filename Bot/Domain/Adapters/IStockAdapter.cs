using System.Threading.Tasks;

namespace Domain.Adapters
{
    public interface IStockAdapter
    {
        public Task<string> GetStocks(string url, string code);
    }
}
