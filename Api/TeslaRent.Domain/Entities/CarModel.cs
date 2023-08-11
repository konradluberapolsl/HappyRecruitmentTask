namespace TeslaRent.Domain.Entities;

public class CarModel
{
    public int Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string Thumbnail { get; set; }
    public int Horsepower { get; set; }
    public int Range { get; set; }
    public int Acceleration { get; set; }
}