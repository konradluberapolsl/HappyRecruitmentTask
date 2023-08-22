using Microsoft.AspNetCore.Mvc;
using TeslaRent.Application.Location.Abstraction;
using TeslaRent.Application.Location.Models;

namespace TeslaRent.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<LocationVm> GetLocations()
    {
        return await _locationService.GetLocations();
    }
}