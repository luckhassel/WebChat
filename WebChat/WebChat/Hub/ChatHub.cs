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
        public async Task SendMessage(string user, string message, string room, bool join, bool leave)
        {
            if (_messages.IsStockBotMessage(message))
            {
                await _stocks.GetStocks(_stocks.GetStockCode(message));
                var messageReceived = _broker.GetMessage();
                if (messageReceived != null)
                    await Clients.Group(room).SendAsync("SendMessage", messageReceived.User, messageReceived.Content);
            }
            else
            {
                if (join)
                {
                    await JoinRoom(room).ConfigureAwait(false);
                }
                else if (leave)
                {
                    await LeaveRoom(room);
                }
                else
                {
                    var date = DateTime.Now;
                    await Clients.Group(room).SendAsync("SendMessage", user, message, date, room);
                    _messagesRepository.AddMessage(new Message { User = user, Content = message, Date = date, Room = room });
                    _messagesRepository.Save();
                }
            }
        }

        public Task JoinRoom(string room)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, room);
        }
        
        public Task LeaveRoom(string room)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
        }
    }
}
