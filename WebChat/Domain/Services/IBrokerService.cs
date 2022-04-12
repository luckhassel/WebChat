using Domain.Entities;

namespace Domain.Services
{
    public interface IBrokerService
    {
        public void AddMessage(Message message);
        public Message GetMessage();
    }
}
