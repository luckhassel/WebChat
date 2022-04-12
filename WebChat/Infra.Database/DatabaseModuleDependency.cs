using Domain.Adapters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infra.Database
{
    public static class DatabaseModuleDependency
    {
        public static void AddDabaseModule(this IServiceCollection service)
        {
            service.AddScoped<IMessagesRepositoryAdapter, MessagesRepository>();
            service.AddScoped<IUsersRepositoryAdapter, UsersRepository>();
        }
    }
}
