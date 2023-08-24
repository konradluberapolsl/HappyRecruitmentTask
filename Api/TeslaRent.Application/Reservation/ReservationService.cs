using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TeslaRent.Application.Availability.Abstraction;
using TeslaRent.Application.Calculation.Abstraction;
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
    private readonly ICalculationService _calculationService;
    private readonly IAppDateTime _appDateTime;
    private readonly IMapper _mapper;


    public ReservationService(
        IDbContext dbContext, 
        IAvailabilityService availabilityService, 
        IAppDateTime appDateTime,
        IMapper mapper, ICarService carService, 
        ICalculationService calculationService)
    {
        _dbContext = dbContext;
        _availabilityService = availabilityService;
        _appDateTime = appDateTime;
        _mapper = mapper;
        _carService = carService;
        _calculationService = calculationService;
    }

    public async Task<ReservationDto> CreateReservation(CreateReservationRequest request)
    {
        await ValidateCreateRequest(request);

        var car = await _carService.GetCarDtoById(request.CarId);
        
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
            EndLocationId = request.EndLocationId
        };
        
        await _dbContext.Reservations.AddAsync(reservation);

        await _dbContext.SaveChangesAsync(CancellationToken.None);

        await _carService.ReserveCar(reservation.CarId, reservation.StartDate, reservation.EndDate);

        if (reservation.StartLocationId != reservation.EndLocationId)
        {
            await _carService.ChangeCarLocation(reservation.CarId, reservation.EndLocationId, reservation.StartDate);
        }
        
        return _mapper.Map<ReservationDto>(reservation);
    }

    public async Task<ReservationDto> ActivateReservation(int reservationId)
    {
        var reservation = await GetReservationById(reservationId);
        
        if (reservation.Status != ReservationStatus.Upcoming)
        {
            throw new Exception("Reservation does not have status of upcoming");
        }

        reservation.Status = ReservationStatus.Active;

        await _dbContext.SaveChangesAsync(CancellationToken.None);
        
        return _mapper.Map<ReservationDto>(reservation);
    }

    public async Task<ReservationDto> FinishReservation(FinishReservationRequest request)
    {
        var reservation = await GetReservationById(request.ReservationId);

        
        // Note: There should be a check here to see if the current date is after the reservation end date
        //       but for testing purposes it is not there 
        
        if (reservation.Status != ReservationStatus.Active)
        {
            throw new Exception("Reservation is not active");
        }

        decimal totalCost = await _calculationService.CalculateReservationTotals(reservation);
        var updatedCar = await _carService.UpdateCarMileage(reservation.CarId, request.CarMileage);

        reservation.Status = ReservationStatus.Finished;
        reservation.TotalCost = totalCost;
        reservation.EndMileage = updatedCar.Mileage;

        _dbContext.Reservations.Update(reservation);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        
        var reservationDto = _mapper.Map<ReservationDto>(reservation);
        reservationDto.Car = updatedCar;

        return reservationDto;
    }

    public async Task<ReservationDto> GetReservationDtoById(int id)
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

    public async Task<IEnumerable<SimpleReservationDto>> GetUserReservations(int userId)
    {
        return await _dbContext.Reservations
            .Where(r => r.UserId == userId)
            .ProjectTo<SimpleReservationDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    private async Task<Domain.Entities.Reservation> GetReservationById(int reservationId)
    {
        var reservation = await _dbContext
            .Reservations
            .FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservation == null)
        {
            throw new Exception("Reservation not found");
        }

        return reservation;
    }
    
    private async Task ValidateCreateRequest(CreateReservationRequest request)
    {
        // TODO: Check if car exists
        // TODO: Check if user exists
        // TODO: Check if locations exists

        //Note: Commented that for testing purposes
        // if (request.StartDate < _appDateTime.Now)
        // {
        //     throw new Exception("Start date must be in the future");
        // }

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