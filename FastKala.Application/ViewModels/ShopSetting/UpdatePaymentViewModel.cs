using FastKala.Domain.Enums.OnlinePayment;

namespace FastKala.Application.ViewModels.ShopSetting;

public class UpdatePaymentViewModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required PaymentType Type { get; set; }
    public required string ApiKey { get; set; }
}