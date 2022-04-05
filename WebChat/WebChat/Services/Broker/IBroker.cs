using WebChat.Entities;

namespace WebChat.Services.Broker
{
    public interface IBroker
    {
        public void AddMessage(Message message);
        public Message GetMessage();
    }
}
