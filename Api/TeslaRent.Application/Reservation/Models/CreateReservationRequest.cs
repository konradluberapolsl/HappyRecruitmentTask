namespace TeslaRent.Application.Reservation.Models;

public class CreateReservationRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StartLocationId { get; set; }
    public int EndLocationId { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
}