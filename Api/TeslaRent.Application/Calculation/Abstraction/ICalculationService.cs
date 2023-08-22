namespace TeslaRent.Application.Calculation.Abstraction;

public interface ICalculationService
{
    Task<decimal> CalculateReservationTotals(Domain.Entities.Reservation reservation);
}