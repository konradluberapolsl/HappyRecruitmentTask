using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeslaRent.Domain.Entities;
using TeslaRent.Domain.Enums;

namespace TeslaRent.Infrastructure.DAL.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.Property(r => r.Status)
            .HasConversion(new EnumToStringConverter<ReservationStatus>());

        builder.HasOne(r => r.StartLocation)
            .WithMany()
            .HasForeignKey(x => x.StartLocationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(r => r.EndLocation)
            .WithMany()
            .HasForeignKey(x => x.EndLocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}