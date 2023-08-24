using TeslaRent.API.Auth.Models;
using TeslaRent.Application.Common.Abstraction;

namespace TeslaRent.API.Auth.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == CustomClaims.UserId);
            
            var validId = int.TryParse(userIdClaim?.Value, out var id);
            return validId ? id : 0;
        }
    }
    
    public string Email => _httpContextAccessor.HttpContext?.User.Claims
        .FirstOrDefault(c => c.Type == CustomClaims.Email)?.Value ?? string.Empty;
}