namespace FastKala.Application.ViewModels.Orders;

public class TapinRequestPriceProductModel
{
    public int count { get; set; }
    public int discount { get; set; }
    public int? price { get; set; }
    public string? title { get; set; }
    public int? weight { get; set; }
    public int? product_id { get; set; }
}

public class TapinRequestPriceModel
{
    public string shop_id { get; set; }
    public string address { get; set; }
    public int city_code { get; set; }
    public int province_code { get; set; }
    public string? description { get; set; }
    public string? email { get; set; }
    public int employee_code { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string mobile { get; set; }
    public string? phone { get; set; }
    public string postal_code { get; set; }
    public int pay_type { get; set; }
    public int order_type { get; set; }
    public int package_weight { get; set; }
    public int box_id { get; set; }
    public List<TapinRequestPriceProductModel> products { get; set; }
}