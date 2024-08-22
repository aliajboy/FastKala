namespace FastKala.Application.ViewModels.OnlinePayment;

public class RequestPaymentVerifyModel
{
    public required string merchant_id { get; set; }
    public required int amount { get; set; }
    public required string authority { get; set; }
}