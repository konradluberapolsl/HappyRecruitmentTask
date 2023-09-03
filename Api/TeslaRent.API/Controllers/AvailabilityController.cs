using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeslaRent.Application.Availability.Abstraction;
using TeslaRent.Application.Availability.Models;
using TeslaRent.Application.Cars.Models;
using TeslaRent.Application.Common.Abstraction;

namespace TeslaRent.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AvailabilityController : ControllerBase
{
    private readonly IAvailabilityService _availabilityService;
    private readonly ICurrentUserService _currentUserService;

    public AvailabilityController(IAvailabilityService availabilityService, ICurrentUserService currentUserService)
    {
        _availabilityService = availabilityService;
        _currentUserService = currentUserService;
    }
    
    [HttpGet("cars")]
    [Authorize]
    public async Task<CarsVm> GetAvailableCarsByLocationAndTimeRange(int locationId, DateTime startDate, DateTime endDate)
    {
        return await _availabilityService.GetAvailableCarsByLocationAndTimeRange(locationId, startDate, endDate);
    }
    
    /*[HttpGet("user/{id}/unavailableRentalDates")]
    public async Task<IEnumerable<DateTimeRangeDto>> GetUnavailableRentalDatesForUser(int id)
    {
        return await _availabilityService.GetUnavailableRentalDatesForUser(id);
    }*/
    
    [HttpGet("user/unavailableRentalDates")]
    [Authorize]
    public async Task<IEnumerable<DateTimeRangeDto>> GetUnavailableRentalDatesForUser()
    {
        return await _availabilityService.GetUnavailableRentalDatesForUser(_currentUserService.UserId);
    }
}