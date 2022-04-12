using Domain.Adapters.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public interface IMessagesService
    {
        public string ConvertToMessage(StockToReceiveDTO stockModel);
        public StockToReceiveDTO ConvertToStock(string response);
    }
}
