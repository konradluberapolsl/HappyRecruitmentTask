using Microsoft.AspNetCore.Mvc;
using TeslaRent.Application.Availability.Abstraction;
using TeslaRent.Application.Availability.Models;
using TeslaRent.Application.Cars.Models;

namespace TeslaRent.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AvailabilityController : ControllerBase
{
    private readonly IAvailabilityService _availabilityService;

    public AvailabilityController(IAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }
    
    [HttpGet("cars")]
    public async Task<CarsVm> GetAvailableCarsByLocationAndTimeRange(int locationId, DateTime startDate, DateTime endDate)
    {
        return await _availabilityService.GetAvailableCarsByLocationAndTimeRange(locationId, startDate, endDate);
    }
    
    [HttpGet("user/{id}/unavailableRentalDates")]
    public async Task<IEnumerable<DateTimeRangeDto>> GetUnavailableRentalDatesForUser(int id)
    {
        return await _availabilityService.GetUnavailableRentalDatesForUser(id);
    }
}