using Microsoft.EntityFrameworkCore;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Application.Common.Abstraction;

public interface IDbContext
{
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<Car> Cars { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}