using TeslaRent.Domain.Enums;

namespace TeslaRent.Domain.Entities;

public class CarStatusHistory
{
    public int Id { get; set; }

    public int CarId { get; set; }
    public Car Car { get; set; }

    public CarStatus Status { get; set; }
    
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}