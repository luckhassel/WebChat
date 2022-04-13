using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IStockService
    {
        public Task GetStocks(string url, string code);

        public string GetStockCode(string message);
    }
}
