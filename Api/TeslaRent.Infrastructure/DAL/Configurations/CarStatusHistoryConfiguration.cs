using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeslaRent.Domain.Entities;
using TeslaRent.Domain.Enums;

namespace TeslaRent.Infrastructure.DAL.Configurations;

public class CarStatusHistoryConfiguration : IEntityTypeConfiguration<CarStatusHistory>
{
    public void Configure(EntityTypeBuilder<CarStatusHistory> builder)
    {
        builder.Property(c => c.Status)
             .HasDefaultValue(CarStatus.Available)
             .HasConversion(new EnumToStringConverter<CarStatus>());
    }
}