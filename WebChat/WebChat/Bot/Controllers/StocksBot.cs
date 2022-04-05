using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;
using WebChat.Bot.Models;
using WebChat.Bot.Services;

namespace WebChat.Bot.Controllers
{
    [ApiController]
    [Route("/api/stocks")]
    public class StocksBot : ControllerBase
    {

        private readonly IStocksBotService _stocks;
        private readonly string _botName;
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "messages";

        public StocksBot(IStocksBotService stocks)
        {
            _stocks = stocks;
            _botName = "StockBot";
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<StockToSendDTO>> Index(string code)
        {
            string messageToSend;
            try
            {
                var stockData = await _stocks.GetStocks(code);
                messageToSend = _stocks.ConvertToMessage(stockData);
            }
            catch
            {
                messageToSend = $"Error trying to get stock data for {code}";
            }

            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare
                        (
                            queue: QUEUE_NAME,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                        );
                    var stringfiedMessage = JsonConvert.SerializeObject(new StockToSendDTO { User = _botName, Content = messageToSend });
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesMessage
                        );
                }
            }

            return Ok(new StockToSendDTO { User = _botName, Content = messageToSend });
        }
    }
}
