using Newtonsoft.Json;

namespace FastKala.Application.ViewModels.OnlinePayment;

public class Data
{
    public int code { get; set; }
    public string message { get; set; } = null!;
    public string authority { get; set; } = null!;
    public string fee_type { get; set; } = null!;
    public int fee { get; set; }
}

public class RequestPaymentResponseModel
{
    [JsonConverter(typeof(JsonSingleOrEmptyArrayConverter<Data>))]
    public Data? data { get; set; }
    [JsonConverter(typeof(JsonSingleOrEmptyArrayConverter<ZarinpalError>))]
    public ZarinpalError? errors { get; set; }
}