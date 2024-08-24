namespace FastKala.Application.ViewModels.Orders;

public class TapinResponsePriceEntriesModel
{
    public int send_price { get; set; }
    public int send_price_tax { get; set; }
    public int service_price { get; set; }
    public int service_price_tax { get; set; }
    public int post_service_price { get; set; }
    public int post_service_price_tax { get; set; }
    public int sms_price { get; set; }
    public int sms_price_tax { get; set; }
    public int logistic_price { get; set; }
    public int logistic_price_tax { get; set; }
    public int total_send_price { get; set; }
    public int total_service_price { get; set; }
    public int total_price { get; set; }
    public int total_weight { get; set; }
}

public class TapinResponsePriceReturnsModel
{
    public int status { get; set; }
    public string message { get; set; }
}

public class TapinResponsePriceModel
{
    public TapinResponsePriceReturnsModel returns { get; set; }
    public TapinResponsePriceEntriesModel entries { get; set; }
}