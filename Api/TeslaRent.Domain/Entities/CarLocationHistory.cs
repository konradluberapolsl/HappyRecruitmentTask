namespace TeslaRent.Domain.Entities;

public class CarLocationHistory
{
    public int Id { get; set; }
    
    public int CarId { get; set; }
    public Car Car { get; set; }

    public int LocationId { get; set; }
    public Location Location { get; set; }

    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}