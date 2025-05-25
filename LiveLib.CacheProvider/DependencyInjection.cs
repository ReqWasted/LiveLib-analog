using LiveLib.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LiveLib.CacheProvider
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(configuration["Redis:Connection"]!);
            });

            services.AddScoped<ICacheProvider>(provider =>
            {
                return new RedisCacheProvider(provider.GetRequiredService<IConnectionMultiplexer>());
            });

            return services;
        }
    }
}
