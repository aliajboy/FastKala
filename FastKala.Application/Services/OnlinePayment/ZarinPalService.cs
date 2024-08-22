using FastKala.Application.Interfaces.OnlinePayment;
using FastKala.Application.ViewModels.OnlinePayment;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace FastKala.Application.Services.OnlinePayment;

public class ZarinPalService : IZarinPalService
{
    private readonly IOptions<ZarinpalConfig> _config;
    private readonly IHttpClientFactory _httpClient;

    public ZarinPalService(IOptions<ZarinpalConfig> config, IHttpClientFactory httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<RequestPaymentResponseModel> RequestPayment(int amount, string description, string mobile, string? orderId = null)
    {
        using var client = _httpClient.CreateClient("zarinpal");

        var parameters = new RequestPaymentModel()
        {
            callback_url = _config.Value.callbackURL,
            amount = amount,
            description = description,
            currency = _config.Value.currency,
            merchant_id = _config.Value.merchantId,
            metadata = new RequestPaymentMetaDataModel()
            {
                mobile = mobile,
                order_id = orderId
            }
        };

        var result = await client.PostAsync("pg/v4/payment/request.json", new StringContent(System.Text.Json.JsonSerializer.Serialize(parameters), Encoding.UTF8, "application/json"));
        return JsonConvert.DeserializeObject<RequestPaymentResponseModel>(await result.Content.ReadAsStringAsync());
    }

    public async Task<RequestPaymentResponseModel> RequestPaymentWithOrderIdReturnURL(int amount, string description, string mobile, string? orderId = null)
    {
        using var client = _httpClient.CreateClient("zarinpal");

        var parameters = new RequestPaymentModel()
        {
            callback_url = _config.Value.callbackURL + orderId,
            amount = amount,
            description = description,
            currency = _config.Value.currency,
            merchant_id = _config.Value.merchantId,
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

        var parameters = new RequestPaymentVerifyModel()
        {
            authority = authority,
            merchant_id = _config.Value.merchantId,
            amount = amount
        };

        var result = await client.PostAsync("pg/v4/payment/verify.json", new StringContent(System.Text.Json.JsonSerializer.Serialize(parameters), Encoding.UTF8, "application/json"));
        return JsonConvert.DeserializeObject<RequestPaymentVerifyResponseModel>(await result.Content.ReadAsStringAsync());
    }


    public async Task<RequestPaymentResponseModel> TestRequestPayment(int amount, string description, string mobile, string? orderId = null)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://sandbox.zarinpal.com/");
            var parameters = new RequestPaymentModel()
            {
                callback_url = _config.Value.callbackURL,
                amount = amount,
                description = description,
                currency = _config.Value.currency,
                merchant_id = _config.Value.merchantId,
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

    public async Task<RequestPaymentResponseModel> TestRequestPaymentWithOrderIdReturnURL(int amount, string description, string mobile, string? orderId = null)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://sandbox.zarinpal.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var parameters = new RequestPaymentModel()
            {
                callback_url = _config.Value.callbackURL + orderId,
                amount = amount,
                description = description,
                currency = _config.Value.currency,
                merchant_id = _config.Value.merchantId,
                metadata = new RequestPaymentMetaDataModel()
                {
                    mobile = mobile,
                    order_id = orderId
                }
            };
            var result = await client.PostAsJsonAsync("pg/v4/payment/request.json", parameters);
            return JsonConvert.DeserializeObject<RequestPaymentResponseModel>(await result.Content.ReadAsStringAsync());
        }
    }

    public async Task<RequestPaymentVerifyResponseModel> TestVerifyPayment(string authority, int amount)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://sandbox.zarinpal.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var parameters = new RequestPaymentVerifyModel()
            {
                authority = authority,
                merchant_id = _config.Value.merchantId,
                amount = amount
            };

            var result = await client.PostAsJsonAsync("pg/v4/payment/verify.json", parameters);
            return JsonConvert.DeserializeObject<RequestPaymentVerifyResponseModel>(await result.Content.ReadAsStringAsync());
        }
    }

}