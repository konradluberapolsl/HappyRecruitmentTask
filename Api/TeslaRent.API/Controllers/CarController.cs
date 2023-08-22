using Microsoft.AspNetCore.Mvc;
using TeslaRent.Application.Cars.Abstraction;
using TeslaRent.Application.Cars.Models;

namespace TeslaRent.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpPost]
    public async Task<CarDto> CreateCar(CreateCarRequest request)
    {
        return await _carService.CreateCar(request);
    }
}