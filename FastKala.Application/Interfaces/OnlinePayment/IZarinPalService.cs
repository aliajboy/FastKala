using FastKala.Application.ViewModels.OnlinePayment;

namespace FastKala.Application.Interfaces.OnlinePayment;

public interface IZarinPalService
{
    Task<RequestPaymentResponseModel> RequestPayment(long amount, string description, string mobile, string callBackUrl, string? orderId = null);

    Task<RequestPaymentVerifyResponseModel> VerifyPayment(string authority, int amount);

    Task<RequestPaymentResponseModel> TestRequestPayment(int amount, string description, string mobile, string callBackUrl, string? orderId = null);

    Task<RequestPaymentVerifyResponseModel> TestVerifyPayment(string authority, int amount);
}