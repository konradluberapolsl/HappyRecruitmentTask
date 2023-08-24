using AutoMapper;
using TeslaRent.Application.Reservation.Models;

namespace TeslaRent.Application.Common.AutoMapper;

public class CustomMappingProfile : Profile
{
    public CustomMappingProfile()
    {
        CreateMap<Domain.Entities.Reservation, SimpleReservationDto>()
            .ForMember(r => r.StartLocationName,
                o => o.MapFrom(s => $"{s.StartLocation.Name}"))
            .ForMember(r => r.EndLocationName,
                o => o.MapFrom(s => $"{s.EndLocation.Name}"))
            .ForMember(r => r.CarModel,
                o => o.MapFrom(s => $"{s.Car.Model.Manufacturer} {s.Car.Model.Model}"));
    }
}