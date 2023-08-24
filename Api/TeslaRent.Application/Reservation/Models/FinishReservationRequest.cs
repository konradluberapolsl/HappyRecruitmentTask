namespace TeslaRent.Application.Reservation.Models;

public class FinishReservationRequest
{
    public int ReservationId { get; set; }
    public double CarMileage { get; set; }
}