using Microsoft.EntityFrameworkCore;
using TeslaRent.Domain.Entities;
using TeslaRent.Domain.Enums;

namespace TeslaRent.Infrastructure.DAL;

public class TeslaRentDbContextInitializer
{
    private readonly TeslaRentDbContext _dbContext;

    public TeslaRentDbContextInitializer(TeslaRentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InitDatabase()
    {
        await _dbContext.Database.MigrateAsync();
    }

    public async Task SeedDatabase()
    {
        if (!_dbContext.CarModels.Any())
        {
            var models = new List<CarModel>()
            {
                new()
                {
                    Manufacturer = "Tesla",
                    Model = "S",
                    Acceleration = 3.20,
                    Horsepower = 691,
                    Range = 634,
                    Thumbnail = "https://www.marinoperformancemotors.com/imagetag/13948/16/l/Used-2022-Tesla-Model-S-Plaid.jpg",
                    CostPerDay = 50,
                    CostPerWeek = 320,
                    CostPerMonth = 1400
                },
                new()
                {
                    Manufacturer = "Tesla",
                    Model = "S Plaid",
                    Acceleration = 2.1,
                    Horsepower = 1020,
                    Range = 600,
                    Thumbnail = "https://www.marinoperformancemotors.com/imagetag/13948/16/l/Used-2022-Tesla-Model-S-Plaid.jpg",
                    CostPerDay = 80,
                    CostPerWeek = 500,
                    CostPerMonth = 2240
                },
            };

            await _dbContext.CarModels.AddRangeAsync(models);
            await _dbContext.SaveChangesAsync();
        }

        if (!_dbContext.Cars.Any())
        {
            var teslaS = await _dbContext.CarModels.FirstOrDefaultAsync(c => c.Model == "S");
            
            var teslaPlaid = await _dbContext.CarModels.FirstOrDefaultAsync(c => c.Model == "S Plaid");
            
            var cars = new List<Car>()
            {
                new ()
                {
                    Model = teslaS,
                    Mileage = 200000,
                    ProductionDate = new DateTime(2021, 01, 01),
                    Vin = "5YJSA1E62MF447249"
                }
            };

            await _dbContext.Cars.AddRangeAsync(cars);
            await _dbContext.SaveChangesAsync();
        }

        if (!_dbContext.Locations.Any())
        {
            var locations = new List<Location>()
            {
                new()
                {
                    Name = "Palma Airport"
                },
                new()
                {
                    Name = "Palma City Center"
                },
                new()
                {
                    Name = "Alcudia"
                },
                new()
                {
                    Name = "Manacor"
                },
            };

            await _dbContext.Locations.AddRangeAsync(locations);
            await _dbContext.SaveChangesAsync();
        }
    }
}