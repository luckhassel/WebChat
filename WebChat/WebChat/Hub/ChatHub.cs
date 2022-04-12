using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebChat.Settings;

namespace WebChat
{
    public class ChatHub : Hub
    {
        private readonly IMessagesService _messages;
        private readonly IBrokerService _broker;
        private readonly IStockService _stocks;
        private readonly IMessagesRepositoryService _messagesRepository;
        private readonly IWebChatConfiguration _configuration;

        public ChatHub(IMessagesService messages, IBrokerService broker, IStockService stocks, 
            IMessagesRepositoryService messagesRepository, IWebChatConfiguration configuration)
        {
            _configuration = configuration;
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
                await _stocks.GetStocks(_configuration.BotUrl, _stocks.GetStockCode(message));
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
                    await _messagesRepository.Save();
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
