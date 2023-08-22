using TeslaRent.Application.Reservation.Models;

namespace TeslaRent.Application.Reservation.Abstractions;

public interface IReservationService
{
    Task<ReservationDto> CreateReservation(CreateReservationRequest request);
    Task<ReservationDto> GetReservation(int id);
    Task<IEnumerable<ReservationDto>> GetUserReservations(int userId);
}