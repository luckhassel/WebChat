using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Adapters
{
    public interface IBrokerAdapter
    {
        public void AddToQueue(string host, string queue, string message);
    }
}
