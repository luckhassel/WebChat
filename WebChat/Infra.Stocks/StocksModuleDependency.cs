﻿using Domain.Adapters;
using Infra.Stocks.Operations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infra.Stocks
{
    public static class StocksModuleDependency
    {
        public static void AddStocksModule(this IServiceCollection service)
        {
            service.AddScoped<IStocksAdapter, StocksManager>();
        }
    }
}