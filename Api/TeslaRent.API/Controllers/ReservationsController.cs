using Microsoft.AspNetCore.Mvc;
using TeslaRent.Application.Reservation.Abstractions;
using TeslaRent.Application.Reservation.Models;

namespace TeslaRent.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    public async Task<ReservationDto> CreateReservation(CreateReservationRequest request)
    {
        return await _reservationService.CreateReservation(request);
    }

    [HttpGet("{id}")]
    public async Task<ReservationDto> GetReservationById(int id)
    {
        return await _reservationService.GetReservationDtoById(id);
    }
    
    [HttpGet("user/{userId}")]
    public async Task<IEnumerable<ReservationDto>> GetReservationsByUserId(int userId)
    {
        return await _reservationService.GetUserReservations(userId);
    }
    
    [HttpPost("/activate")]
    public async Task<ReservationDto> FinishReservation(int reservationId)
    {
        return await _reservationService.ActivateReservation(reservationId);
    }

    [HttpPost("/finish")]
    public async Task<ReservationDto> FinishReservation(FinishReservationRequest request)
    {
        return await _reservationService.FinishReservation(request);
    }
}