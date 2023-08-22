using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TeslaRent.Application.Cars.Abstraction;
using TeslaRent.Application.Cars.Models;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Application.Location;
using TeslaRent.Application.Location.Models;
using TeslaRent.Domain.Entities;
using TeslaRent.Domain.Enums;

namespace TeslaRent.Application.Cars;

public class CarService : ICarService
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IAppDateTime _appDateTime;

    public CarService(IDbContext dbContext, IMapper mapper, IAppDateTime appDateTime)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _appDateTime = appDateTime;
    }

    public async Task<CarDto> GetCarById(int id)
    {
        var car = await _dbContext
            .Cars
            .FirstOrDefaultAsync(c => c.Id == id);

        if (car == null)
        {
            throw new Exception("Car not found");
        }

        return _mapper.Map<CarDto>(car);
    }

    public async Task<CarDto> CreateCar(CreateCarRequest request)
    {
        var carStatus = new CarStatusHistory
        {
            Status = CarStatus.Available,
            FromDate = _appDateTime.Now,
            ToDate = null
        };


        //TODO: Add validation for location
        var carLocation = new CarLocationHistory()
        {
            LocationId = request.LocationId,
            FromDate = _appDateTime.Now,
            ToDate = null
        };

        var car = new Car()
        {
            Vin = request.Vin,
            CarModelId = request.CarModelId,
            Mileage = request.Mileage,
            ProductionDate = request.ProductionDate,
            CarStatusHistory = new List<CarStatusHistory>
            {
                carStatus
            },
            CarLocationHistory = new List<CarLocationHistory>()
            {
                carLocation
            }
        };

        await _dbContext.Cars.AddAsync(car);

        await _dbContext.SaveChangesAsync(CancellationToken.None);

        return _mapper.Map<CarDto>(car);
    }

    public async Task<CarStatusDto> ReserveCar(int carId, DateTime fromDate, DateTime toDate)
    {
        var statuses = await GetStatusAtTimeRange(carId, fromDate, toDate);

        if (!statuses.Any() || statuses.Count > 1 )
        {
            throw new Exception("Status cannot be changed");
        }
        
        var currentStatus = statuses.First();

        // if (currentStatus.ToDate.HasValue && !toDate.HasValue)
        // {
        //     throw new Exception("Original status is of limited duration");
        //     //can be problematic
        // }

        if (currentStatus.Status != CarStatus.Available)
        {
            throw new Exception("Car is not available");
        }

        var status = new CarStatusHistory()
        {
            CarId = carId,
            Status = CarStatus.Reserved,
            FromDate = fromDate,
            ToDate = toDate
        };

        var complementaryStatus = new CarStatusHistory()
        {
            CarId = carId,
            Status = currentStatus.Status,
            FromDate = toDate,
            ToDate = currentStatus.ToDate
        };
        
        currentStatus.ToDate = fromDate;
        
        await _dbContext.CarStatusHistory.AddAsync(status);
        await _dbContext.CarStatusHistory.AddAsync(complementaryStatus);
        _dbContext.CarStatusHistory.Update(currentStatus);
        
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        return _mapper.Map<CarStatusDto>(status);
    }

    public async Task ChangeCarLocation(int carId, int newLocationId, DateTime fromDate)
    {
        var currentLocation = await GetCurrentCarLocation(carId);

        if (currentLocation.LocationId == newLocationId)
        {
            return;
        }

        if (currentLocation.FromDate > fromDate)
        {
            throw new Exception("Can not change location");
        }

        currentLocation.ToDate = fromDate;

        var newLocation = new CarLocationHistory()
        {
            CarId = carId,
            LocationId = newLocationId,
            FromDate = fromDate,
            ToDate = null
        };

        _dbContext.CarLocationHistory.Update(currentLocation);
        await _dbContext.CarLocationHistory.AddAsync(newLocation);
        
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    private async Task<ICollection<CarStatusHistory>> GetStatusAtTimeRange(int carId, DateTime fromDate, DateTime toDate)
    {
        if (toDate < fromDate)
        {
            throw new Exception("toDate can not be earlier than from date");
        }
        
        var statuses = await _dbContext.CarStatusHistory
            .Where(s => 
                s.CarId == carId 
                && s.FromDate <= fromDate
                && (!s.ToDate.HasValue || s.ToDate >= toDate))
            .OrderBy(s => s.FromDate)
            .ToListAsync();
        
        return statuses;
    }

    private async Task<CarLocationHistory> GetCurrentCarLocation(int carId)
    {
        var locations = await _dbContext.CarLocationHistory
            .Where(s => 
                s.CarId == carId
                && !s.ToDate.HasValue
                )
            .ToListAsync();

        if (!locations.Any())
        {
            throw new Exception("Car in nowhere");
        }
        
        if (locations.Count > 1)
        {
            throw new Exception("Car is in more then one place at the time");
        }
        
        return locations.Single();
    }
}