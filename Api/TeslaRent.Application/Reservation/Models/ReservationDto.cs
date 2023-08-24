using TeslaRent.Application.Cars.Models;
using TeslaRent.Application.Common.AutoMapper;
using TeslaRent.Application.Location.Models;
using TeslaRent.Application.Users.Models;
using TeslaRent.Domain.Enums;

namespace TeslaRent.Application.Reservation.Models;

public class ReservationDto : IMapFrom<Domain.Entities.Reservation>
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public ReservationStatus Status { get; set; }
    public double StartMileage { get; set; }
    public double? EndMileage { get; set; }
    public decimal? TotalCost { get; set; }
    
    public LocationDto StartLocation { get; set; }
    public LocationDto EndLocation { get; set; }
    
    public UserDto User { get; set; }
    
    public CarDto Car { get; set; }
}