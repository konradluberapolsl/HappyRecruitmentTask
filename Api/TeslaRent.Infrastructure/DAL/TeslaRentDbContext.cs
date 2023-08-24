using Microsoft.EntityFrameworkCore;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Infrastructure.DAL;

public class TeslaRentDbContext : DbContext, IDbContext
{
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarStatusHistory> CarStatusHistory { get; set; }
    public DbSet<CarLocationHistory> CarLocationHistory { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public TeslaRentDbContext(DbContextOptions<TeslaRentDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}