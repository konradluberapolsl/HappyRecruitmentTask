using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeslaRent.Infrastructure.DAL;

namespace TeslaRent.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        
        return services;
    }
}