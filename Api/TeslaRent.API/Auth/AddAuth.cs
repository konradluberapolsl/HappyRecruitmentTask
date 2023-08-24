using Microsoft.AspNetCore.Authentication.JwtBearer;
using TeslaRent.API.Auth.Models;

namespace TeslaRent.API.Auth;

public static class AddAuth
{
    public static IServiceCollection AddAuth0Authentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var auth0 = configuration.GetSection("Auth0").Get<Auth0Options>();
        if (auth0 is null)
        {
            throw new ArgumentNullException(nameof(Auth0Options),"Invalid Auth0 configuration");
        }
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = auth0.Issuer;
            options.Audience = auth0.Audience;
        });
        
        return services;
    }
}