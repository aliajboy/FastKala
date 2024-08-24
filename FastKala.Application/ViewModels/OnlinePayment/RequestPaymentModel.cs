using System.Text.Json.Serialization;

namespace FastKala.Application.ViewModels.OnlinePayment;

public class RequestPaymentModel
{
    public required string merchant_id { get; set; }
    public required long amount { get; set; }
    public string? currency { get; set; } = "IRT";
    public required string description { get; set; }
    public required string callback_url { get; set; }
    public required RequestPaymentMetaDataModel metadata { get; set; }
}

public class RequestPaymentMetaDataModel
{
    public required string mobile { get; set; }
    [JsonIgnore]
    public string? email { get; set; }
    [JsonIgnore]
    public string? order_id { get; set; }
}