using Microsoft.EntityFrameworkCore;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Application.Common.Abstraction;

public interface IDbContext
{
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarStatusHistory> CarStatusHistory { get; set; }
    public DbSet<CarLocationHistory> CarLocationHistory { get; set; }
    public DbSet<Domain.Entities.Location> Locations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Domain.Entities.Reservation> Reservations { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}