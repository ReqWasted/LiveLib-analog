using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiveLib.Application
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            services.AddTransient<IRunnerFabric, RunnerFabric>();

            services.AddScoped<IRunnerService, RunnerService>();

            services.AddScoped<ISimulationService, SimulationService>();

            services.AddScoped<IProbabilityService, ProbabilityService>();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }

}
