using TeslaRent.Application.Cars.Models;

namespace TeslaRent.Application.Cars.Abstraction;

public interface ICarService
{
    Task<CarDto> CreateCar(CreateCarRequest request);
    Task<CarStatusDto> ReserveCar(int carId, DateTime fromDate, DateTime toDate);
    Task ChangeCarLocation(int carId, int newLocationId, DateTime fromDate);
    Task<CarDto> UpdateCarMileage(int carId, double newMileage);
    Task<CarDto> GetCarDtoById(int id);
}