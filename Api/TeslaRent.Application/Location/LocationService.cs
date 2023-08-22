using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Application.Location.Abstraction;
using TeslaRent.Application.Location.Models;

namespace TeslaRent.Application.Location;

public class LocationService : ILocationService
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;


    public LocationService(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<LocationVm> GetLocations()
    {
        var locations = await _dbContext.Locations
            .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new()
        {
            Locations = locations,
            Count = locations.Count
        };
    }
}