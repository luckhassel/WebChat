using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Settings
{
    public class BrokerConfiguration:IBrokerConfiguration
    {
        private readonly IConfiguration _configuration;
        private RabbitMqConfiguration rabbitMqConfig;
        public BrokerConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string StoqUrl => _configuration["StoqUrl"];

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
