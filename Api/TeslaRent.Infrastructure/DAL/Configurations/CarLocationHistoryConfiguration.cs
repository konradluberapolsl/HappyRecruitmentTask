using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Infrastructure.DAL.Configurations;

public class CarLocationHistoryConfiguration : IEntityTypeConfiguration<CarLocationHistory>
{
    public void Configure(EntityTypeBuilder<CarLocationHistory> builder)
    {
    }
}