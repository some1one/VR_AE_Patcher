using System;
using GameFinder.StoreHandlers.Steam.Models.ValueTypes;
using Microsoft.Extensions.DependencyInjection;
using VR_AE_Patcher.Interfaces;

namespace VR_AE_Patcher.IoC;

public static class ServiceCollectionFactory
{
    public static ServiceCollection Build() {
        ServiceCollection services = new();
        //services.AddSingleton<IModCache, ModCache>();
        return services;
    }
}
