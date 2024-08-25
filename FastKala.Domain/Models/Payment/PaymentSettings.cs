using FastKala.Domain.Enums.OnlinePayment;

namespace FastKala.Domain.Models.Payment;

public record PaymentSettings
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public PaymentType Type { get; set; }
    public string ApiKey { get; set; } = null!;
    public Currency Currency { get; set; }
    public string CartNumber { get; set; } = null!;
    public string AccountNumber { get; set; } = null!;
    public string ShebaNumber { get; set; } = null!;
}