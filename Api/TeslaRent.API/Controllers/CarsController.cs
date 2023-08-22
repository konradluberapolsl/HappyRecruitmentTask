using Microsoft.AspNetCore.Mvc;
using TeslaRent.Application.Cars.Abstraction;
using TeslaRent.Application.Cars.Models;

namespace TeslaRent.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpPost]
    public async Task<CarDto> CreateCar(CreateCarRequest request)
    {
        return await _carService.CreateCar(request);
    }

    [HttpGet("/{id}")]
    public async Task<CarDto> GetById(int id)
    {
        return await _carService.GetCarDtoById(id);
    }
}