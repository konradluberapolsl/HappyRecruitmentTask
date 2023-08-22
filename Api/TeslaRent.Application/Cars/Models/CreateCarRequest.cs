using TeslaRent.Application.Common.AutoMapper;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Application.Cars.Models;

public class CreateCarRequest
{
    public string Vin { get; set; }
    public int CarModelId { get; set; }
    public double Mileage { get; set; }
    public DateTime ProductionDate { get; set; }

    public int LocationId { get; set; }
}