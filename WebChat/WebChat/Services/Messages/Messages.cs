namespace WebChat.Services.Messages
{
    public class Messages : IMessages
    {
        public bool IsStockBotMessage(string message)
        {
            return message.StartsWith("/stock=");
        }
    }
}
