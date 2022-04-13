using Microsoft.Extensions.Configuration;
using System;

namespace WebChat.Settings
{
    public class WebChatConfiguration : IWebChatConfiguration
    {
        private readonly IConfiguration _configuration;
        private RabbitMqConfiguration rabbitMqConfig;
        public WebChatConfiguration(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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
