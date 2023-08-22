using TeslaRent.Application.Cars.Models;

namespace TeslaRent.Application.Cars.Abstraction;

public interface ICarService
{
    Task<CarDto> CreateCar(CreateCarRequest request);
    Task<CarStatusDto> ReserveCar(int carId, DateTime fromDate, DateTime toDate);
    Task<CarDto> GetCarById(int id);
}