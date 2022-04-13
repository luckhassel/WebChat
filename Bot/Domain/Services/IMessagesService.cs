using Domain.Adapters.Entities;

namespace Domain.Services
{
    public interface IMessagesService
    {
        public string ConvertToMessage(StockToReceiveDTO stockModel);
        public StockToReceiveDTO ConvertToStock(string response);
    }
}
