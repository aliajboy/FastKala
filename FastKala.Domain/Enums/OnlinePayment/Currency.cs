using System.ComponentModel.DataAnnotations;

namespace FastKala.Domain.Enums.OnlinePayment;

public enum Currency : byte
{
    [Display(Name = "تومان")]
    IRT = 1,
    [Display(Name = "ریال")]
    IRR = 2
}