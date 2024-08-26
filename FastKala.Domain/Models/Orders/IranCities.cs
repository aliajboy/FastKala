namespace FastKala.Domain.Models.Orders;

public record IranCities
{
    public int Id { get; set; }
    public string? State { get; set; }
    public int StateId { get; set; }
    public string? City { get; set; }
    public int CityId { get; set; }
}
