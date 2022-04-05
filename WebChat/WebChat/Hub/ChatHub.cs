using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebChat.Entities;
using WebChat.Services.Broker;
using WebChat.Services.Messages;
using WebChat.Services.Stocks;


namespace WebChat
{
    public class ChatHub : Hub
    {
        private readonly IMessages _messages;
        private readonly IStocks _stocks;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IBroker _broker;
        public ChatHub(IMessages messages, IStocks stocks, IMessagesRepository messagesRepository,
            IBroker broker)
        {
            _broker = broker;
            _messages = messages;
            _stocks = stocks;
            _messagesRepository = messagesRepository;
        }

        [Authorize]
        public async Task SendMessage(string user, string message)
        {
            if (_messages.IsStockBotMessage(message))
            {
                await _stocks.GetStocks(_stocks.GetStockCode(message));
                var _message = _broker.GetMessage();
                if (_message != null)
                    await Clients.All.SendAsync("SendMessage", _message.User, _message.Content);
            }
            else
            {
                var date = DateTime.Now;
                await Clients.All.SendAsync("SendMessage", user, message, date);
                _messagesRepository.AddMessage(new Message { User = user, Content = message, Date = date });
                _messagesRepository.Save();
            }
        }
    }
}
