using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Application.Reservation.Abstractions;
using TeslaRent.Application.Reservation.Models;

namespace TeslaRent.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly ICurrentUserService _currentUserService;

    public ReservationsController(IReservationService reservationService, ICurrentUserService currentUserService)
    {
        _reservationService = reservationService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ReservationDto> CreateReservation(CreateReservationRequest request)
    {
        return await _reservationService.CreateReservation(request);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ReservationDto> GetReservationById(int id)
    {
        return await _reservationService.GetReservationDtoById(id);
    }
    
    /*
    [HttpGet("user/{userId}")]
    public async Task<IEnumerable<SimpleReservationDto>> GetReservationsByUserId(int userId)
    {
        return await _reservationService.GetUserReservations(userId);
    }*/
    
    [HttpGet("user")]
    [Authorize]
    public async Task<IEnumerable<SimpleReservationDto>> GetReservationsForLoggedInUser()
    {
        return await _reservationService.GetUserReservations(_currentUserService.UserId);
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