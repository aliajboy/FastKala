using FastKala.Domain.Enums.Orders;

namespace FastKala.Domain.Models.Orders;

public record Orders
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime DateTimeCreated { get; set; } = new DateTime();
    public DateTime? DateTimePaid { get; set; }
    public DateTime? DateTimeCompleted { get; set; }
    public string CustomerId { get; set; } = null!;
    public string CustomerFirstName { get; set; } = null!;
    public string CustomerLastName { get; set; } = null!;
    public string CustomerTown { get; set; } = null!;
    public string CustomerCity { get; set; } = null!;
    public string CustomerAddress { get; set; } = null!;
    public string? CustomerEmail { get; set; }
    public string CustomerPhone { get; set; } = null!;
    public string? CustomerNote { get; set; }
    public byte PaymentMethod { get; set; }
    public string? TransactionId { get; set; }
    public string? CartNumber { get; set; }
    public long TotalPrice { get; set; } = 0;
    public long TotalTax { get; set; } = 0;
    public long TotalShipping { get; set; } = 0;
    public int ShippingTypeId { get; set; }
    public string? Authority { get; set; }
}