using Bot.Settings;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebChat.Bot.Controllers
{
    [ApiController]
    [Route("/api/stocks")]
    public class StocksBotController : ControllerBase
    {
        private readonly IBrokerService _broker;
        private readonly IMessagesService _message;
        private readonly IStocksService _stock;
        private readonly IBrokerConfiguration _configuration;
        private readonly string _errorMessage;
        public StocksBotController(IBrokerService broker, IMessagesService message,
            IStocksService stock, IBrokerConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _broker = broker ?? throw new ArgumentNullException(nameof(broker));
            _stock = stock ?? throw new ArgumentNullException(nameof(stock));
            _message = message ?? throw new ArgumentNullException(nameof(message));
            _errorMessage = "Error trying to retrieve stock";
        }

        [HttpGet("{code}")]
        public async Task<ActionResult> ForwardStock(string code)
        {
            string messageToSend;
            var stockMessage = await _stock.GetStocks(_configuration.StoqUrl, code);
            try
            {
                var stockMessageFormatted = _message.ConvertToStock(stockMessage);
                messageToSend = _message.ConvertToMessage(stockMessageFormatted);
            }
            catch
            {
                messageToSend = $"{_errorMessage} {code}";
            }

            _broker.AddToQueue(_configuration.RabbitConfig.Host, _configuration.RabbitConfig.Queue, messageToSend);

            return Accepted();
        }
    }
}
