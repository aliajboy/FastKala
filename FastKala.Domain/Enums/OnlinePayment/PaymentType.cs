using System.ComponentModel.DataAnnotations;

namespace FastKala.Domain.Enums.OnlinePayment;

public enum PaymentType
{
    [Display(Name = "زرین پال")]
    ZarinPal = 1,
    [Display(Name = "سداد")]
    Sedad = 2,
    [Display(Name = "Pay.ir")]
    PayIr = 3,
    [Display(Name = "نکست پی")]
    NextPay = 4,
    [Display(Name = "به پرداخت ملت")]
    BehPardakht = 5,
    [Display(Name = "سامان کیش")]
    SamanKish = 6,
    [Display(Name = "پارسیان")]
    Parsian = 7,
    [Display(Name = "پاسارگاد")]
    Pasargad = 8,
    [Display(Name = "کارت به کارت")]
    CartbeCart = 9
}