using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Infrastructure.AppDateTime;
using TeslaRent.Infrastructure.DAL;

namespace TeslaRent.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        
        services.AddSingleton<IAppDateTime, UtcAppDateTime>();
        
        return services;
    }
}