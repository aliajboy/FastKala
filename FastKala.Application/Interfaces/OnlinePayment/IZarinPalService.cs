using FastKala.Application.ViewModels.OnlinePayment;

namespace FastKala.Application.Interfaces.OnlinePayment;

public interface IZarinPalService
{
    Task<RequestPaymentResponseModel> RequestPayment(int amount, string description, string mobile, string? orderId = null);

    Task<RequestPaymentResponseModel> RequestPaymentWithOrderIdReturnURL(int amount, string description, string mobile, string? orderId = null);

    Task<RequestPaymentVerifyResponseModel> VerifyPayment(string authority, int amount);

    Task<RequestPaymentResponseModel> TestRequestPayment(int amount, string description, string mobile, string? orderId = null);

    Task<RequestPaymentResponseModel> TestRequestPaymentWithOrderIdReturnURL(int amount, string description, string mobile, string? orderId = null);

    Task<RequestPaymentVerifyResponseModel> TestVerifyPayment(string authority, int amount);
}