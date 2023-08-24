using TeslaRent.Domain.Enums;

namespace TeslaRent.Application.Reservation.Models;

public class SimpleReservationDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ReservationStatus Status { get; set; }
    public string StartLocationName { get; set; }
    public string EndLocationName { get; set; }
    public string CarModel { get; set; }
}