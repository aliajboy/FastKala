using FastKala.Domain.Models.Payment;

namespace FastKala.Application.ViewModels.ShopSettings;

public class PaymentSettingsViewModel
{
    public List<PaymentSettings> PaymentSettings { get; set; } = new List<PaymentSettings>();
    public PaymentSettings Payment { get; set; } = new PaymentSettings();
}
