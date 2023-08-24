using TeslaRent.Application.Availability.Models;
using TeslaRent.Application.Cars.Models;

namespace TeslaRent.Application.Availability.Abstraction;

public interface IAvailabilityService
{
    Task<CarsVm> GetAvailableCarsByLocationAndTimeRange(int locationId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<DateTimeRangeDto>> GetUnavailableRentalDatesForUser(int userId);
    Task<bool> IsCarAvailable(int carId, DateTime startDate, DateTime endDate);
    Task<bool> IsCarAtLocation(int carId, int locationId, DateTime startDate, DateTime endDate);

}