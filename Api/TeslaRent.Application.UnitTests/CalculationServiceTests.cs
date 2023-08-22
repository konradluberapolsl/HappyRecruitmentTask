using Moq.EntityFrameworkCore;
using TeslaRent.Application.Calculation;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Application.UnitTests;

public class CalculationServiceTests
{
    private static readonly Fixture Fixture = new Fixture();

    public CalculationServiceTests()
    {
        Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior(3));
    }

    [Fact]
    public async void CalculateReservationTotals_RentalWithZeroDayDuration_ReturnsPriceForOneDay()
    {
        // Arrange
        int carId = 1;
        decimal costPerDay = 10;
        decimal costPerWeek = 60;
        decimal costPerMonth = 90;

        var reservation = Fixture.Build<Domain.Entities.Reservation>()
            .With(r => r.StartDate, new DateTime(2023, 01, 01))
            .With(r => r.EndDate, new DateTime(2023, 01, 01))
            .With(r => r.CarId, carId).Create();

        //var carsMock = MockingHelpers.CreateDbSetMock<Car>();
        var dbContextMock = new Mock<IDbContext>();
        dbContextMock.SetupGet(c => c.Cars).ReturnsDbSet(GenerateCars(carId, costPerDay, costPerMonth, costPerWeek));

        var calculationService = new CalculationService(dbContextMock.Object);

        // Act
        var price = await calculationService.CalculateReservationTotals(reservation);

        // Assert
        price.Should().Be(costPerDay);
    }

    [Theory]
    [InlineData(1, 1, 1, 5, 40)]
    [InlineData(1, 1, 1, 8, 60)]
    [InlineData(1, 1, 1, 15, 120)]
    [InlineData(1, 1, 1, 14, 120)]
    [InlineData(1, 1, 1, 31, 90)]
    [InlineData(1, 1, 2, 1, 100)]
    [InlineData(1, 1, 3, 2, 180)]
    public async void CalculateReservationTotals_RentalWithGivenDuration_ReturnsCorrectPrice(int startMonth,
        int startDay, int endMonth, int endDay, decimal expected)
    {
        // Arrange
        int carId = 1;
        decimal costPerDay = 10;
        decimal costPerWeek = 60;
        decimal costPerMonth = 90;

        var reservation = Fixture.Build<Domain.Entities.Reservation>()
            .With(r => r.StartDate, new DateTime(2023, startMonth, startDay))
            .With(r => r.EndDate, new DateTime(2023, endMonth, endDay))
            .With(r => r.CarId, carId).Create();

        var dbContextMock = new Mock<IDbContext>();
        dbContextMock.SetupGet(c => c.Cars).ReturnsDbSet(GenerateCars(carId, costPerDay, costPerWeek, costPerMonth));

        var calculationService = new CalculationService(dbContextMock.Object);

        // Act
        var price = await calculationService.CalculateReservationTotals(reservation);

        // Assert
        price.Should().Be(expected);
    }


    private static IList<Car> GenerateCars(int carId, decimal costPerDay, decimal costPerWeek, decimal costPerMonth)
    {
        IList<Car> cars = new List<Car>()
        {
            Fixture.Build<Car>()
                .With(c => c.Id, carId)
                .Without(c => c.CarModelId)
                .Without(c => c.Model)
                .Do(c =>
                {
                    c.Model = Fixture.Build<CarModel>().With(cm => cm.CostPerDay, costPerDay)
                        .With(cm => cm.CostPerWeek, costPerWeek)
                        .With(cm => cm.CostPerMonth, costPerMonth)
                        .Create();
                    c.CarModelId = c.Model.Id;
                })
                .Create()
        };

        return cars;
    }
}