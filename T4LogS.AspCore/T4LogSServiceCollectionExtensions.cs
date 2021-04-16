using Microsoft.Extensions.DependencyInjection;
using System;
using T4LogS.Core;

namespace T4LogS.AspCore
{
    public static class T4LogSServiceCollectionExtensions
    {
        public static IServiceCollection AddT4LogS(this IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection AddT4LogS(this IServiceCollection services, Action<T4LogS.Core.T4LogSOptions> configure)
        {
            configure?.Invoke(new T4LogSOptions());
            return services;
        }
    }
}
