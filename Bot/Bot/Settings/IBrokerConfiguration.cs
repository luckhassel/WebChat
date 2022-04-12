using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Settings
{
    public interface IBrokerConfiguration
    {
        public string StoqUrl { get; }
        public RabbitMqConfiguration RabbitConfig { get; }
    }
}
