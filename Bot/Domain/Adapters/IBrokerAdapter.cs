namespace Domain.Adapters
{
    public interface IBrokerAdapter
    {
        public void AddToQueue(string host, string queue, string message);
    }
}
