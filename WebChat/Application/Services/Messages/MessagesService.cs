using Domain.Services;

namespace Application.Services
{
    public class MessagesService : IMessagesService
    {
        public bool IsStockBotMessage(string message)
        {
            return message.StartsWith("/stock=");
        }
    }
}
