using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TeslaRent.Application.Availability.Abstraction;
using TeslaRent.Application.Availability.Models;
using TeslaRent.Application.Cars.Models;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Domain.Enums;

namespace TeslaRent.Application.Availability;

public class AvailabilityService : IAvailabilityService
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IAppDateTime _appDateTime;

    public AvailabilityService(IDbContext dbContext, IMapper mapper, IAppDateTime appDateTime)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _appDateTime = appDateTime;
    }

    public async Task<CarsVm> GetAvailableCarsByLocationAndTimeRange(int locationId, DateTime startDate, DateTime endDate)
    {
        var cars = await _dbContext.CarLocationHistory
            .Join(_dbContext.CarStatusHistory, 
                cl => cl.CarId, 
                cs => cs.CarId,
                (cl, cs) => new {location = cl, status = cs})
            .Where(combined =>
                combined.location.LocationId == locationId 
                && (combined.location.ToDate == null || combined.location.ToDate.Value >= endDate) 
                && combined.location.FromDate <= startDate
                && combined.status.Status == CarStatus.Available
                && (combined.status.ToDate == null || combined.status.ToDate.Value >= endDate) 
                && combined.status.FromDate <= startDate
            )
            .Select(c => c.location.Car)
            .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return new()
        {
            Cars = cars,
            Count = cars.Count
        };
    }

    public async Task<IEnumerable<DateTimeRangeDto>> GetUnavailableRentalDatesForUser(int userId)
    {
        var futureRentals = await _dbContext.Reservations
            .Where(r => r.UserId == userId && r.StartDate >= _appDateTime.Now)
            .ToListAsync();

        return futureRentals.Select(r => new DateTimeRangeDto()
        {
            startDate = r.StartDate,
            endDate = r.EndDate
        });
    }

    public async Task<bool> IsCarAvailable(int carId, DateTime startDate, DateTime endDate)
    {
        return await _dbContext.CarStatusHistory
            .AnyAsync(s =>
                s.CarId == carId
                && s.Status == CarStatus.Available
                && (!s.ToDate.HasValue || s.ToDate.Value >= endDate)
                && s.FromDate <= startDate);
    }

    public async Task<bool> IsCarAtLocation(int carId, int locationId, DateTime startDate, DateTime endDate)
    {
        return await _dbContext.CarLocationHistory
            .AnyAsync(s =>
                s.CarId == carId
                && s.LocationId == locationId
                && (!s.ToDate.HasValue || s.ToDate.Value >= endDate)
                && s.FromDate <= startDate);
    }
}