using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeslaRent.Domain.Entities;
using TeslaRent.Domain.Enums;

namespace TeslaRent.Infrastructure.DAL.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(c => c.Status)
            .HasDefaultValue(CarStatus.Available)
            .HasConversion(new EnumToStringConverter<CarStatus>());
    }
}