using TeslaRent.Application.Common.AutoMapper;

namespace TeslaRent.Application.Location.Models;

public class LocationDto : IMapFrom<Domain.Entities.Location>
{
    public int Id { get; set; }
    public string Name { get; set; }
}