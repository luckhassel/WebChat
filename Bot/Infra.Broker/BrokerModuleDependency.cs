using Domain.Adapters;
using Infra.Broker.Operations;
using Microsoft.Extensions.DependencyInjection;

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
