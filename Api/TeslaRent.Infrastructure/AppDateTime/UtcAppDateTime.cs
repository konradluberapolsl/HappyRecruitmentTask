using TeslaRent.Application.Common.Abstraction;

namespace TeslaRent.Infrastructure.AppDateTime;

public class UtcAppDateTime : IAppDateTime
{
    public DateTime Now => DateTime.UtcNow;
}