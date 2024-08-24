using FastKala.Application.Interfaces.OnlinePayment;
using FastKala.Application.ViewModels.OnlinePayment;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace FastKala.Application.Services.OnlinePayment;

public class ZarinPalService : IZarinPalService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IPaymentService _paymentService;

    public ZarinPalService(IHttpClientFactory httpClient, IPaymentService paymentService)
    {
        _httpClient = httpClient;
        _paymentService = paymentService;
    }

    public async Task<RequestPaymentResponseModel> RequestPayment(long amount, string description, string mobile, string callBackUrl, string? orderId = null)
    {
        using var client = _httpClient.CreateClient("zarinpal");
        var zarinpalData = await _paymentService.GetZarinpalData();

        var parameters = new RequestPaymentModel()
        {
            callback_url = callBackUrl,
            amount = amount,
            description = description,
            currency = zarinpalData.Currency,
            merchant_id = zarinpalData.ApiKey,
            metadata = new RequestPaymentMetaDataModel()
            {
                mobile = mobile,
                order_id = orderId
            }
        };

        var result = await client.PostAsync("pg/v4/payment/request.json", new StringContent(System.Text.Json.JsonSerializer.Serialize(parameters), Encoding.UTF8, "application/json"));
        return JsonConvert.DeserializeObject<RequestPaymentResponseModel>(await result.Content.ReadAsStringAsync());
    }

    public async Task<RequestPaymentVerifyResponseModel> VerifyPayment(string authority, int amount)
    {
        using var client = _httpClient.CreateClient("zarinpal");
        var zarinpalData = await _paymentService.GetZarinpalData();

        var parameters = new RequestPaymentVerifyModel()
        {
            authority = authority,
            merchant_id = zarinpalData.ApiKey,
            amount = amount
        };

        var result = await client.PostAsync("pg/v4/payment/verify.json", new StringContent(System.Text.Json.JsonSerializer.Serialize(parameters), Encoding.UTF8, "application/json"));
        return JsonConvert.DeserializeObject<RequestPaymentVerifyResponseModel>(await result.Content.ReadAsStringAsync());
    }


    public async Task<RequestPaymentResponseModel> TestRequestPayment(int amount, string description, string mobile, string callBackUrl, string? orderId = null)
    {
        var zarinpalData = await _paymentService.GetZarinpalData();
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://sandbox.zarinpal.com/");
            var parameters = new RequestPaymentModel()
            {
                callback_url = callBackUrl,
                amount = amount,
                description = description,
                currency = zarinpalData.Currency,
                merchant_id = zarinpalData.ApiKey,
                metadata = new RequestPaymentMetaDataModel()
                {
                    mobile = mobile,
                    order_id = orderId
                }
            };
            var result = await client.PostAsync("pg/v4/payment/request.json", new StringContent(System.Text.Json.JsonSerializer.Serialize(parameters), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<RequestPaymentResponseModel>(await result.Content.ReadAsStringAsync());
        }
    }

    public async Task<RequestPaymentVerifyResponseModel> TestVerifyPayment(string authority, int amount)
    {
        var zarinpalData = await _paymentService.GetZarinpalData();
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://sandbox.zarinpal.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var parameters = new RequestPaymentVerifyModel()
            {
                authority = authority,
                merchant_id = zarinpalData.ApiKey,
                amount = amount
            };

            var result = await client.PostAsJsonAsync("pg/v4/payment/verify.json", parameters);
            return JsonConvert.DeserializeObject<RequestPaymentVerifyResponseModel>(await result.Content.ReadAsStringAsync());
        }
    }

}