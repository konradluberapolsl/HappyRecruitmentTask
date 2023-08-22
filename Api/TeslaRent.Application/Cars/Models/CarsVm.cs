namespace TeslaRent.Application.Cars.Models;

public class CarsVm
{
    public IReadOnlyCollection<CarDto> Cars { get; set; }
    public int Count { get; set; }
}