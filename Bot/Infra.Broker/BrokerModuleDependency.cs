using Domain.Adapters;
using Infra.Broker.Operations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infra.Broker
{
    public static class BrokerModuleDependency
    {
        public static void AddBrokerModule(this IServiceCollection service)
        {
            service.AddTransient<IBrokerAdapter, BrokerManager>();
        }
    }
}
