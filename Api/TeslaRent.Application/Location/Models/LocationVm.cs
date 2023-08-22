namespace TeslaRent.Application.Location.Models;

public class LocationVm
{
    public IReadOnlyCollection<LocationDto> Locations { get; set; }
    public int Count { get; set; }    
}