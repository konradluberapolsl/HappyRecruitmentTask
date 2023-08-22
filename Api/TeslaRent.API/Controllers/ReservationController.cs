using Microsoft.AspNetCore.Mvc;
using TeslaRent.Application.Reservation.Abstractions;
using TeslaRent.Application.Reservation.Models;

namespace TeslaRent.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
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
        return await _reservationService.GetReservation(id);
    }
}