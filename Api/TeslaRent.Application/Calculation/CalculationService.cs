using Microsoft.EntityFrameworkCore;
using TeslaRent.Application.Calculation.Abstraction;
using TeslaRent.Application.Calculation.Models;
using TeslaRent.Application.Common.Abstraction;

namespace TeslaRent.Application.Calculation;

public class CalculationService : ICalculationService
{
    private readonly IDbContext _dbContext;

    public CalculationService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<decimal> CalculateReservationTotals(Domain.Entities.Reservation reservation)
    {
        var carPricing = await GetCarPricing(reservation.CarId);

        int rentalDays = GetNumberOfDays(reservation.StartDate, reservation.EndDate);

        decimal totalPrice = 0;

        if (rentalDays == 0)
        {
            rentalDays = 1;
        }

        int numberOfMonths = rentalDays / 30;

        if (numberOfMonths > 0)
        {
            totalPrice += numberOfMonths * carPricing.CostPerMonth;
            rentalDays -= numberOfMonths * 30;
        }

        int numberOfWeeks = rentalDays / 7;

        if (numberOfWeeks > 0)
        {
            totalPrice += numberOfWeeks * carPricing.CostPerWeek;
            rentalDays -= numberOfWeeks * 7;
        }

        int numberOfDays = rentalDays;
        if (numberOfDays < 0)
        {
            throw new Exception("Calculation went wrong");
        }

        totalPrice += numberOfDays * carPricing.CostPerDay;

        return totalPrice;
    }

    private async Task<CarPricingDto> GetCarPricing(int carId)
    {
        var carModel = await _dbContext.Cars
            .Where(c => c.Id == carId)
            .Select(c => c.Model)
            .FirstOrDefaultAsync();

        if (carModel == null)
        {
            throw new Exception("Can not find car pricing");
        }

        return new()
        {
            CostPerDay = carModel.CostPerDay,
            CostPerWeek = carModel.CostPerWeek,
            CostPerMonth = carModel.CostPerMonth
        };
    }

    private int GetNumberOfDays(DateTime startDate, DateTime endDate)
    {
        return Convert.ToInt32((endDate - startDate).TotalDays);
    }
}