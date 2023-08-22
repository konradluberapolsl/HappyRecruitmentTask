using TeslaRent.Application.Common.AutoMapper;
using TeslaRent.Domain.Entities;
using TeslaRent.Domain.Enums;

namespace TeslaRent.Application.Cars.Models;

public class CarStatusDto : IMapFrom<CarStatusHistory>
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public CarStatus Status { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}