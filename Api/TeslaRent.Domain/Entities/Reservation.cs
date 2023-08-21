using TeslaRent.Domain.Enums;

namespace TeslaRent.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public ReservationStatus Status { get; set; }

    public int StartLocationId { get; set; }
    public Location StartLocation { get; set; }
    
    public int EndLocationId { get; set; }
    public Location EndLocation { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}