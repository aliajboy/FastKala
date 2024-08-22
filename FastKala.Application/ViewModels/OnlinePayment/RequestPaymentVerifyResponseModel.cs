using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FastKala.Application.ViewModels.OnlinePayment;

public class RequestPaymentVerifyResponseModel
{
    [JsonConverter(typeof(JsonSingleOrEmptyArrayConverter<VerifyData>))]
    public VerifyData? data { get; set; }
    [JsonConverter(typeof(JsonSingleOrEmptyArrayConverter<ZarinpalError>))]
    public ZarinpalError? errors { get; set; }
}

public class VerifyData
{
    public int code { get; set; }
    public string message { get; set; }
    public string card_hash { get; set; }
    public string card_pan { get; set; }
    public int ref_id { get; set; }
    public string fee_type { get; set; }
    public int fee { get; set; }
}

public class ZarinpalError
{
    public VerifyPaymentError code { get; set; }
    public string message { get; set; }
}

public enum VerifyPaymentError
{
    [Display(Name = "مبلغ پرداخت شده با مقدار مبلغ ارسالی در متد وریفای متفاوت است")]
    ValidationError = -50,
    [Display(Name = "پرداخت ناموفق")]
    Failed = -51,
    [Display(Name = "خطای غیر منتظره‌ای رخ داده است. لطفا این خطا را به پشتیبانی اطلاع دهید")]
    UnExpected = -52,
    [Display(Name = "پرداخت متعلق به این وبسایت نیست")]
    WrongMerchant = -53,
    [Display(Name = "اتوریتی نامعتبر است")]
    InvalidAuthority = -54,
    [Display(Name = "تراکنش مورد نظر یافت نشد")]
    NotFound = -55,
    [Display(Name = "خطایی رخ داده است. به امور مشتریان زرین پال اطلاع دهید")]
    WagesError = -39,
    [Display(Name = "درگاه پرداخت به حالت تعلیق در آمده است")]
    Suspend = -15,
    [Display(Name = "تلاش بیش از دفعات مجاز")]
    TooMany = -12,
    [Display(Name = "مرچنت کد فعال نیست")]
    MerchantErro = -11
}

public class JsonSingleOrEmptyArrayConverter<T> : JsonConverter where T : class
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(T).IsAssignableFrom(objectType);
    }

    public override bool CanWrite { get { return false; } }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var contract = serializer.ContractResolver.ResolveContract(objectType);
        if (!(contract is Newtonsoft.Json.Serialization.JsonObjectContract || contract is Newtonsoft.Json.Serialization.JsonDictionaryContract))
        {
            throw new JsonSerializationException(string.Format("Unsupported objectType {0} at {1}.", objectType, reader.Path));
        }

        switch (reader.SkipComments().TokenType)
        {
            case JsonToken.StartArray:
                {
                    int count = 0;
                    while (reader.Read())
                    {
                        switch (reader.TokenType)
                        {
                            case JsonToken.Comment:
                                break;
                            case JsonToken.EndArray:
                                return existingValue;
                            default:
                                {
                                    count++;
                                    if (count > 1)
                                        throw new JsonSerializationException(string.Format("Too many objects at path {0}.", reader.Path));
                                    existingValue = existingValue ?? contract.DefaultCreator();
                                    serializer.Populate(reader, existingValue);
                                }
                                break;
                        }
                    }
                    // Should not come here.
                    throw new JsonSerializationException(string.Format("Unclosed array at path {0}.", reader.Path));
                }

            case JsonToken.Null:
                return null;

            case JsonToken.StartObject:
                existingValue = existingValue ?? contract.DefaultCreator();
                serializer.Populate(reader, existingValue);
                return existingValue;

            default:
                throw new InvalidOperationException("Unexpected token type " + reader.TokenType.ToString());
        }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}

public static partial class JsonExtensions
{
    public static JsonReader SkipComments(this JsonReader reader)
    {
        while (reader.TokenType == JsonToken.Comment && reader.Read())
            ;
        return reader;
    }
}
