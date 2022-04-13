namespace Domain.Services
{
    public interface IBrokerService
    {
        public void AddToQueue(string host, string queue, string message);
    }
}
