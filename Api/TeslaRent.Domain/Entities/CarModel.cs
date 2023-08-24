namespace TeslaRent.Domain.Entities;

public class CarModel
{
    public int Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string Thumbnail { get; set; }
    public int Horsepower { get; set; }
    public int Range { get; set; }
    public double Acceleration { get; set; }
    public decimal CostPerDay { get; set; }
    public decimal CostPerWeek { get; set; }
    public decimal CostPerMonth { get; set; }
}