using Microsoft.EntityFrameworkCore;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Domain.Entities;
using TeslaRent.Domain.Enums;

namespace TeslaRent.Infrastructure.DAL;

public class TeslaRentDbContextInitializer
{
    private readonly TeslaRentDbContext _dbContext;
    private readonly IAppDateTime _appDateTime;

    public TeslaRentDbContextInitializer(TeslaRentDbContext dbContext, IAppDateTime appDateTime)
    {
        _dbContext = dbContext;
        _appDateTime = appDateTime;
    }

    public async Task InitDatabase()
    {
        await _dbContext.Database.MigrateAsync();
    }

    public async Task SeedDatabase()
    {
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


        if (!_dbContext.CarModels.Any())
        {
            var teslaS = new CarModel()
            {
                Manufacturer = "Tesla",
                Model = "S",
                Acceleration = 3.1,
                Horsepower = 691,
                Range = 375,
                Thumbnail =
                    "https://www.marinoperformancemotors.com/imagetag/13948/16/l/Used-2022-Tesla-Model-S-Plaid.jpg",
                CostPerDay = 50,
                CostPerWeek = 320,
                CostPerMonth = 1400
            };

            var teslaSPlaid = new CarModel()
            {
                Manufacturer = "Tesla",
                Model = "S Plaid",
                Acceleration = 1.99,
                Horsepower = 1020,
                Range = 348,
                Thumbnail =
                    "https://www.marinoperformancemotors.com/imagetag/13948/16/l/Used-2022-Tesla-Model-S-Plaid.jpg",
                CostPerDay = 80,
                CostPerWeek = 500,
                CostPerMonth = 2240
            };

            var tesla3 = new CarModel()
            {
                Manufacturer = "Tesla",
                Model = "3",
                Acceleration = 5.8,
                Horsepower = 279,
                Range = 272,
                Thumbnail = "https://cdn.motor1.com/images/mgl/y2mbjm/s3/tesla-model-3.jpg",
                CostPerDay = 30,
                CostPerWeek = 200,
                CostPerMonth = 850
            };

            var tesla3LR = new CarModel()
            {
                Manufacturer = "Tesla",
                Model = "3 Long Range",
                Acceleration = 4.2,
                Horsepower = 346,
                Range = 333,
                Thumbnail = "https://cdn.motor1.com/images/mgl/y2mbjm/s3/tesla-model-3.jpg",
                CostPerDay = 32,
                CostPerWeek = 214,
                CostPerMonth = 910
            };

            var tesla3P = new CarModel()
            {
                Manufacturer = "Tesla",
                Model = "3 Performance",
                Acceleration = 3.1,
                Horsepower = 450,
                Range = 315,
                Thumbnail = "https://cdn.motor1.com/images/mgl/y2mbjm/s3/tesla-model-3.jpg",
                CostPerDay = 35,
                CostPerWeek = 235,
                CostPerMonth = 1000
            };

            var teslaX = new CarModel()
            {
                Manufacturer = "Tesla",
                Model = "X",
                Acceleration = 3.8,
                Horsepower = 670,
                Range = 348,
                Thumbnail =
                    "https://v.wpimg.pl/NDA2NjUuYSUgUDhZeg5sMGMIbAM8V2JmNBB0SHpEfHxxSmEHexQ7MC5ZOwYhWz4obkYrGTkUYykuVisGeA1jIiBRKwY8Ezppcx8sWWFFeHJ0HCQaMlcz",
                CostPerDay = 60,
                CostPerWeek = 330,
                CostPerMonth = 1410
            };

            var teslaXPlaid = new CarModel()
            {
                Manufacturer = "Tesla",
                Model = "X Plaid",
                Acceleration = 2.5,
                Horsepower = 1020,
                Range = 333,
                Thumbnail =
                    "https://v.wpimg.pl/NDA2NjUuYSUgUDhZeg5sMGMIbAM8V2JmNBB0SHpEfHxxSmEHexQ7MC5ZOwYhWz4obkYrGTkUYykuVisGeA1jIiBRKwY8Ezppcx8sWWFFeHJ0HCQaMlcz",
                CostPerDay = 90,
                CostPerWeek = 510,
                CostPerMonth = 2250
            };

            var teslaY = new CarModel()
            {
                Manufacturer = "Tesla",
                Model = "Y",
                Acceleration = 5.0,
                Horsepower = 299,
                Range = 279,
                Thumbnail = "https://www.bankier.pl/moto/wp-content/uploads/2021/07/tesla-y-2.jpg",
                CostPerDay = 32,
                CostPerWeek = 214,
                CostPerMonth = 910
            };

            var teslaYLR = new CarModel()
            {
                Manufacturer = "Tesla",
                Model = "Y Long Range",
                Acceleration = 4.8,
                Horsepower = 384,
                Range = 330,
                Thumbnail = "https://www.bankier.pl/moto/wp-content/uploads/2021/07/tesla-y-2.jpg",
                CostPerDay = 35,
                CostPerWeek = 235,
                CostPerMonth = 1000
            };

            var teslaYP = new CarModel()
            {
                Manufacturer = "Tesla",
                Model = "Y Performance",
                Acceleration = 3.5,
                Horsepower = 456,
                Range = 303,
                Thumbnail = "https://www.bankier.pl/moto/wp-content/uploads/2021/07/tesla-y-2.jpg",
                CostPerDay = 36,
                CostPerWeek = 242,
                CostPerMonth = 1030
            };

            var now = _appDateTime.Now;
            var locations = await _dbContext.Locations.ToListAsync();

            if (!locations.Any())
            {
                throw new Exception("Can not seed cars because there are no locations");
            }

            var cars = new List<Car>()
            {
                new()
                {
                    Model = teslaS,
                    Mileage = 1150,
                    ProductionDate = new DateTime(2022, 02, 01),
                    Vin = "5YJSA1E5XNF475700",
                    CarStatusHistory = new List<CarStatusHistory>()
                    {
                        new()
                        {
                            Status = CarStatus.Available,
                            FromDate = now,
                            ToDate = null
                        }
                    },
                    CarLocationHistory = new List<CarLocationHistory>()
                    {
                        new()
                        {
                            LocationId = locations.First().Id,
                            FromDate = now,
                            ToDate = null
                        }
                    }
                },
                new()
                {
                    Model = teslaSPlaid,
                    Mileage = 2000,
                    ProductionDate = new DateTime(2022, 01, 01),
                    Vin = "5YJSA1E62MF447249",
                    CarStatusHistory = new List<CarStatusHistory>()
                    {
                        new()
                        {
                            Status = CarStatus.Available,
                            FromDate = now,
                            ToDate = null
                        }
                    },
                    CarLocationHistory = new List<CarLocationHistory>()
                    {
                        new()
                        {
                            LocationId = locations.First().Id,
                            FromDate = now,
                            ToDate = null
                        }
                    }
                },
                new()
                {
                    Model = tesla3,
                    Mileage = 555,
                    ProductionDate = new DateTime(2023, 01, 01),
                    Vin = "5YJ3E1EA1PF563950",
                    CarStatusHistory = new List<CarStatusHistory>()
                    {
                        new()
                        {
                            Status = CarStatus.Available,
                            FromDate = now,
                            ToDate = null
                        }
                    },
                    CarLocationHistory = new List<CarLocationHistory>()
                    {
                        new()
                        {
                            LocationId = locations.First().Id,
                            FromDate = now,
                            ToDate = null
                        }
                    }
                },
                new()
                {
                    Model = tesla3LR,
                    Mileage = 1256,
                    ProductionDate = new DateTime(2023, 05, 11),
                    Vin = "5YJ3E1EB3NF269052",
                    CarStatusHistory = new List<CarStatusHistory>()
                    {
                        new()
                        {
                            Status = CarStatus.Available,
                            FromDate = now,
                            ToDate = null
                        }
                    },
                    CarLocationHistory = new List<CarLocationHistory>()
                    {
                        new()
                        {
                            LocationId = locations.First().Id,
                            FromDate = now,
                            ToDate = null
                        }
                    }
                },
                new()
                {
                    Model = tesla3P,
                    Mileage = 11256,
                    ProductionDate = new DateTime(2022, 04, 09),
                    Vin = "5YJ3E1ECXPF394187",
                    CarStatusHistory = new List<CarStatusHistory>()
                    {
                        new()
                        {
                            Status = CarStatus.Available,
                            FromDate = now,
                            ToDate = null
                        }
                    },
                    CarLocationHistory = new List<CarLocationHistory>()
                    {
                        new()
                        {
                            LocationId = locations.First().Id,
                            FromDate = now,
                            ToDate = null
                        }
                    }
                },
                new()
                {
                    Model = teslaX,
                    Mileage = 12345,
                    ProductionDate = new DateTime(2023, 02, 14),
                    Vin = "7SAXCDE53PF383805",
                    CarStatusHistory = new List<CarStatusHistory>()
                    {
                        new()
                        {
                            Status = CarStatus.Available,
                            FromDate = now,
                            ToDate = null
                        }
                    },
                    CarLocationHistory = new List<CarLocationHistory>()
                    {
                        new()
                        {
                            LocationId = locations.First().Id,
                            FromDate = now,
                            ToDate = null
                        }
                    }
                },
                new()
                {
                    Model = teslaXPlaid,
                    Mileage = 430,
                    ProductionDate = new DateTime(2022, 09, 14),
                    Vin = "7SAXCBE61NF345600",
                    CarStatusHistory = new List<CarStatusHistory>()
                    {
                        new()
                        {
                            Status = CarStatus.Available,
                            FromDate = now,
                            ToDate = null
                        }
                    },
                    CarLocationHistory = new List<CarLocationHistory>()
                    {
                        new()
                        {
                            LocationId = locations.First().Id,
                            FromDate = now,
                            ToDate = null
                        }
                    }
                },
                new()
                {
                    Model = teslaY,
                    Mileage = 1140,
                    ProductionDate = new DateTime(2022, 04, 03),
                    Vin = "5YJYGDEE2MF180101",
                    CarStatusHistory = new List<CarStatusHistory>()
                    {
                        new()
                        {
                            Status = CarStatus.Available,
                            FromDate = now,
                            ToDate = null
                        }
                    },
                    CarLocationHistory = new List<CarLocationHistory>()
                    {
                        new()
                        {
                            LocationId = locations.First().Id,
                            FromDate = now,
                            ToDate = null
                        }
                    }
                },
                new()
                {
                    Model = teslaYLR,
                    Mileage = 16321,
                    ProductionDate = new DateTime(2022, 01, 03),
                    Vin = "7SAYGAEE0NF307852",
                    CarStatusHistory = new List<CarStatusHistory>()
                    {
                        new()
                        {
                            Status = CarStatus.Available,
                            FromDate = now,
                            ToDate = null
                        }
                    },
                    CarLocationHistory = new List<CarLocationHistory>()
                    {
                        new()
                        {
                            LocationId = locations.First().Id,
                            FromDate = now,
                            ToDate = null
                        }
                    }
                },
                new()
                {
                    Model = teslaYP,
                    Mileage = 1400,
                    ProductionDate = new DateTime(2023, 08, 14),
                    Vin = "5YJ3E1ECXPF394187",
                    CarStatusHistory = new List<CarStatusHistory>()
                    {
                        new()
                        {
                            Status = CarStatus.Available,
                            FromDate = now,
                            ToDate = null
                        }
                    },
                    CarLocationHistory = new List<CarLocationHistory>()
                    {
                        new()
                        {
                            LocationId = locations.First().Id,
                            FromDate = now,
                            ToDate = null
                        }
                    }
                },
            };

            await _dbContext.Cars.AddRangeAsync(cars);
            await _dbContext.SaveChangesAsync();
        }
    }
}