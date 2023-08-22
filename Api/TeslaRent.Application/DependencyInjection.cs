using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TeslaRent.Application.Availability;
using TeslaRent.Application.Availability.Abstraction;
using TeslaRent.Application.Calculation;
using TeslaRent.Application.Calculation.Abstraction;
using TeslaRent.Application.Cars;
using TeslaRent.Application.Cars.Abstraction;
using TeslaRent.Application.Location;
using TeslaRent.Application.Location.Abstraction;
using TeslaRent.Application.Reservation;
using TeslaRent.Application.Reservation.Abstractions;
using TeslaRent.Application.Users;
using TeslaRent.Application.Users.Abstraction;

namespace TeslaRent.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ILocationService, LocationService>();
        services.AddTransient<ICarService, CarService>();
        services.AddTransient<IAvailabilityService, AvailabilityService>();
        services.AddTransient<IReservationService, ReservationService>();
        services.AddTransient<ICalculationService, CalculationService>();

        return services;
    }
}