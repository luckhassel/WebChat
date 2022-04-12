using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IStocksService
    {
        public Task<string> GetStocks(string url, string code);
    }
}
