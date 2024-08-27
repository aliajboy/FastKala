using FastKala.Domain.Enums.Orders;
using FastKala.Domain.Models.Orders;
using System.ComponentModel.DataAnnotations;

namespace FastKala.Application.ViewModels.Orders;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "وارد کردن این فیلد الزامیست")]
    public string Name { get; set; } = null!;
    [Required]
    public string Family { get; set; } = null!;
    [Required]
    public int TownId { get; set; }
    [Required]
    public int CityId { get; set; }
    [Required]
    public string Address { get; set; } = null!;
    [Required]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "کدپستی نامعتبر است")]
    public string PostalCode { get; set; } = null!;
    [Required]
    [Phone]
    public string Phone { get; set; } = null!;
    [EmailAddress]
    public string? Email { get; set; }
    public string? Description { get; set; }
    [Required]
    public ShippingMethods ShippingMethod { get; set; }
    [Required]
    public PaymentMethods PaymentMethod { get; set; }
    [Required]
    public long TotalPrice { get; set; }
    public bool AcceptTerms { get; set; }
    public string? Authority { get; set; }
    public string? Error { get; set; }
    public List<IranCities> IranCities { get; set; } = new List<IranCities>();
}