using TeslaRent.Application.Reservation.Models;

namespace TeslaRent.Application.Reservation.Abstractions;

public interface IReservationService
{
    Task<ReservationDto> CreateReservation(CreateReservationRequest request);
    Task<ReservationDto> ActivateReservation(int reservationId);
    Task<ReservationDto> FinishReservation(FinishReservationRequest request);
    Task<ReservationDto> GetReservationDtoById(int id);
    Task<IEnumerable<ReservationDto>> GetUserReservations(int userId);
}