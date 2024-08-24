using FastKala.Domain.Enums.Orders;
using System.ComponentModel.DataAnnotations;

namespace FastKala.Application.ViewModels.Orders;

public class CheckoutViewModel
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Family { get; set; } = null!;
    [Required]
    public string Town { get; set; } = null!;
    [Required]
    public string City { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    [Required]
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
}