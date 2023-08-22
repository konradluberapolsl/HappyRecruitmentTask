using TeslaRent.Domain.Enums;

namespace TeslaRent.Domain.Entities;

public class Car
{
    public int Id { get; set; }
    public string Vin { get; set; }
    public int CarModelId { get; set; }
    public CarModel Model { get; set; }
    public double Mileage { get; set; }
    public DateTime ProductionDate { get; set; }

    public ICollection<CarStatusHistory> CarStatusHistory { get; set; }
    public ICollection<CarLocationHistory> CarLocationHistory { get; set; }
}