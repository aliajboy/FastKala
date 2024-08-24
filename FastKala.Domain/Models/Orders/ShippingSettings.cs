using FastKala.Domain.Enums.Orders;

namespace FastKala.Domain.Models.Orders;

public record ShippingSettings
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ShippingMethods Type { get; set; }
    public bool Active { get; set; }
    public long FreeShippingPrice { get; set; }
    public long Price { get; set; }
}