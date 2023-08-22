using TeslaRent.Application.Common.AutoMapper;
using TeslaRent.Application.Location.Models;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Application.Cars.Models;

public class CarLocationDto : IMapFrom<CarLocationHistory>
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public LocationDto Location { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}