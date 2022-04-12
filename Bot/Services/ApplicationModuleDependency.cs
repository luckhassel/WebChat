using Application.Services;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Services
{
    public static class ApplicationModuleDependency
    {
        public static void AddApplicationModule(this IServiceCollection service)
        {
            service.AddTransient<IMessagesService, MessagesService>();
            service.AddTransient<IStocksService, StocksService>();
            service.AddTransient<IBrokerService, BrokerService>();
        }
    }
}
