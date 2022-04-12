using Application.Services;
using Application.Services.Auth;
using Application.Services.Stocks;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application
{
    public static class ApplicationModuleDependency
    {
        public static void AddApplicationModule(this IServiceCollection service)
        {
            service.AddSingleton<IBrokerService, BrokerService>();
            service.AddScoped<IMessagesService, MessagesService>();
            service.AddScoped<IMessagesRepositoryService, MessagesRepositoryService>();
            service.AddScoped<IUserRepositoryService, UserRepositoryService>();
            service.AddScoped<IStockService, StockService>();
        }
    }
}
