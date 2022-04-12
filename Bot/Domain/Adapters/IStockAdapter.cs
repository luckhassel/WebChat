using Domain.Adapters.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapters
{
    public interface IStockAdapter
    {
        public Task<string> GetStocks(string url, string code);
    }
}
