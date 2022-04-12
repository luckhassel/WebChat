using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public interface IBrokerService
    {
        public void AddToQueue(string host, string queue, string message);
    }
}
