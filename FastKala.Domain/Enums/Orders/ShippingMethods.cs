using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastKala.Domain.Enums.Orders;

public enum ShippingMethods
{
    [Display(Name = "پست")]
    Post = 0,
    [Display(Name = "تیپاکس")]
    Tipax = 1,
    [Display(Name = "پیک فوری")]
    Peyk = 2,
    [Display(Name = "چاپار")]
    Chapar = 3,
    [Display(Name = "باربری")]
    Barbari = 4,
    [Display(Name = "تحویل حضوری")]
    TahvilHozori = 5
}