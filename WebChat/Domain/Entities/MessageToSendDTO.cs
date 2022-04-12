using System;

namespace Domain.Entities
{
    public class MessageToSendDTO
    {
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string User { get; set; }
        public string Room { get; set; }
    }
}
