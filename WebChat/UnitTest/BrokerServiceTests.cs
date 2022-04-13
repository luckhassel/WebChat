using Application.Services;
using Domain.Entities;
using Xunit;

namespace UnitTest
{
    public class BrokerServiceTests
    {

        [Fact]
        public void GetMessageSuccess()
        {
            Message message = new Message { Content = "test" };

            var broker = new BrokerService();
            broker.AddMessage(message);
            var returnMessage = broker.GetMessage();

            Assert.Equal(message, returnMessage);
        }
    }
}
