using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IStockService
    {
        public Task GetStocks(string url, string code);

        public string GetStockCode(string message);
    }
}
