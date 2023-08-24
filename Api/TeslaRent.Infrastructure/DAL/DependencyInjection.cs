using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeslaRent.Application.Common.Abstraction;

namespace TeslaRent.Infrastructure.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TeslaRentDbContext>(options => options.UseSqlServer(connectionString));
        
        services.AddScoped<IDbContext>(provider => provider.GetRequiredService<TeslaRentDbContext>());
        services.AddScoped<TeslaRentDbContextInitializer>();
        
        return services;
    }
}