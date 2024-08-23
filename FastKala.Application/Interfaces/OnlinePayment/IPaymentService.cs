using FastKala.Domain.Models.Payment;

namespace FastKala.Application.Interfaces.OnlinePayment;

public interface IPaymentService
{
    public Task<PaymentSettings> GetZarinpalData();
}