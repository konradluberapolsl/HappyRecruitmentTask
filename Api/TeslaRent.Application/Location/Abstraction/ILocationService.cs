using TeslaRent.Application.Location.Models;

namespace TeslaRent.Application.Location.Abstraction;

public interface ILocationService
{
    Task<IEnumerable<LocationDto>> GetLocations();
}