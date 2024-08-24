using System.ComponentModel.DataAnnotations;

namespace FastKala.Domain.Enums.Orders;

public enum PaymentMethods
{
    [Display(Name = "درگات چرداخت امن زرین پال")]
    ZarinPal = 0,
    [Display(Name = "کارت به کارت")]
    CartBeCart = 1
}