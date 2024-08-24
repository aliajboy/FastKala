namespace FastKala.Domain.Models.Payment;

public record PaymentSettings
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public string Currency { get; set; } = null!;
}