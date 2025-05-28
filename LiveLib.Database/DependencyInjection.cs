using LiveLib.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiveLib.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"]
                ?? throw new ArgumentNullException(nameof(configuration), "ConnectionString is missing in configuration.");

            services.AddDbContext<PostgresDatabaseContext>(options =>
            {
                options.UseNpgsql(connectionString, options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                //options.UseInMemoryDatabase("testdb");
            });

            services.AddScoped<IDatabaseContext>(provider =>
            {


                return provider.GetRequiredService<PostgresDatabaseContext>();
            });




            return services;
        }
    }
}
