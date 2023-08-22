using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeslaRent.Application.Availability.Abstraction;
using TeslaRent.Application.Cars.Abstraction;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Application.Reservation.Abstractions;
using TeslaRent.Application.Reservation.Models;
using TeslaRent.Domain.Enums;

namespace TeslaRent.Application.Reservation;

public class ReservationService : IReservationService
{
    private readonly IDbContext _dbContext;
    private readonly IAvailabilityService _availabilityService;
    private readonly ICarService _carService;
    private readonly IAppDateTime _appDateTime;
    private readonly IMapper _mapper;


    public ReservationService(
        IDbContext dbContext, 
        IAvailabilityService availabilityService, 
        IAppDateTime appDateTime,
        IMapper mapper, ICarService carService)
    {
        _dbContext = dbContext;
        _availabilityService = availabilityService;
        _appDateTime = appDateTime;
        _mapper = mapper;
        _carService = carService;
    }

    public async Task<ReservationDto> CreateReservation(CreateReservationRequest request)
    {
        await ValidateRequest(request);

        var car = await _carService.GetCarById(request.CarId);
        
        var reservation = new Domain.Entities.Reservation()
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedDate = _appDateTime.Now,
            Status = ReservationStatus.Upcoming,
            UserId = request.UserId,
            CarId = request.CarId,
            StartMileage = car.Mileage,
            StartLocationId = request.StartLocationId,
            EndLocationId = request.StartLocationId
        };
        
        await _dbContext.Reservations.AddAsync(reservation);

        await _dbContext.SaveChangesAsync(CancellationToken.None);

        await _carService.ReserveCar(reservation.CarId, reservation.StartDate, reservation.EndDate);
        
        // TODO: Change car location
        
        return _mapper.Map<ReservationDto>(reservation);
    }

    public async Task<ReservationDto> GetReservation(int id)
    {
        var reservation = await _dbContext
            .Reservations
            .Include(r => r.Car)
                .ThenInclude(c => c.Model)
            .Include(r => r.StartLocation)
            .Include(r => r.EndLocation)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reservation == null)
        {
            throw new Exception("Reservation not found");
        }

        return _mapper.Map<ReservationDto>(reservation);
    }

    public Task<IEnumerable<ReservationDto>> GetUserReservations(int userId)
    {
        throw new NotImplementedException();
    }

    private async Task ValidateRequest(CreateReservationRequest request)
    {
        // TODO: Check if car exists
        // TODO: Check if user exists
        // TODO: Check if locations exists

        if (request.StartDate < _appDateTime.Now)
        {
            throw new Exception("Start date must be in the future");
        }

        if (request.StartDate > request.EndDate)
        {
            throw new Exception("Cannot set start date after end date");
        }

        if (!await _availabilityService.IsCarAvailable(request.CarId, request.StartDate, request.EndDate))
        {
            throw new Exception("Car is not available at given time");
        }

        if (!await _availabilityService.IsCarAtLocation(request.CarId, request.StartLocationId, request.StartDate,
                request.EndDate))
        {
            throw new Exception("Car is not at given location");
        }
    }
}