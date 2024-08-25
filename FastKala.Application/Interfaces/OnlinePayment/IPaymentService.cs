using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.ShopSettings;
using FastKala.Domain.Models.Payment;

namespace FastKala.Application.Interfaces.OnlinePayment;

public interface IPaymentService
{
    public Task<List<PaymentSettings>?> GetAllPayments();

    public Task<PaymentSettings?> GetZarinpalData();

    public Task<OperationResult> UpdateGateway(UpdatePaymentViewModel updatePayment);

    public Task<OperationResult> AddPayment(PaymentSettings payment);

    public Task<OperationResult> RemovePayment(int paymentId);
}