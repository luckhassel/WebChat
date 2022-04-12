using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Settings
{
    public class WebChatConfiguration : IWebChatConfiguration
    {
        private readonly IConfiguration _configuration;
        private RabbitMqConfiguration rabbitMqConfig;
        public WebChatConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string BotUrl => _configuration["BotUrl"];

        public RabbitMqConfiguration RabbitConfig 
        {
            get
            {
                rabbitMqConfig = new RabbitMqConfiguration(_configuration.GetSection("RabbitMqConfig")["Host"], 
                    _configuration.GetSection("RabbitMqConfig")["Queue"]);
                return rabbitMqConfig;
            }
        }
    }
}
