using TeslaRent.Application.Common.AutoMapper;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Application.Cars.Models;

public class CarDto : IMapFrom<Car>
{
    public int Id { get; set; }
    public string Vin { get; set; }
    public CarModelDto Model { get; set; }
    public double Mileage { get; set; }
    public DateTime ProductionDate { get; set; }

    public ICollection<CarStatusDto> CarStatusHistory { get; set; }
    public ICollection<CarLocationDto> CarLocationHistory { get; set; }
}