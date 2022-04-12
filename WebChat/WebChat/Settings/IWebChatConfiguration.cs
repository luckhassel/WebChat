using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Settings
{
    public interface IWebChatConfiguration
    {
        public string BotUrl { get; }
        public RabbitMqConfiguration RabbitConfig { get; }
    }
}
