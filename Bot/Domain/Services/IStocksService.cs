using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IStocksService
    {
        public Task<string> GetStocks(string url, string code);
    }
}
