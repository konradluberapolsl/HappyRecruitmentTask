using System.Security.Claims;
using TeslaRent.API.Auth.Models;
using TeslaRent.Application.Users.Abstraction;
using TeslaRent.Application.Users.Models;

namespace TeslaRent.API.Auth.Middleware;

public class GetCurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public GetCurrentUserMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(
        HttpContext context,
        IUserService userService)
    {
        var claimEmail = context.User.Claims.FirstOrDefault(c => c.Type == Auth0Claims.Email)?.Value;
        if (claimEmail is null)
        {
            throw new Exception();
        }

        var userByEmail = await userService.GetUserByEmail(claimEmail);
        if (userByEmail is null)
        {
            var user = new CreateUserRequest()
            {
                Email = claimEmail,
                FirstName = "Joe",
                LastName = "Doe"
            };

            await userService.CreateUser(user);
        }

        userByEmail = await userService.GetUserByEmail(claimEmail);
        if (userByEmail is null)
        {
            throw new Exception(); // Hmm
        }

        var userClaims = new List<Claim>
        {
            new(CustomClaims.UserId, userByEmail.Id.ToString()),
            new(CustomClaims.Email, userByEmail.Email)
        };
        context.User?.AddIdentity(new ClaimsIdentity(userClaims));
        
        await _next(context);
    }
}