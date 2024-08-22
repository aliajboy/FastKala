using FastKala.Domain.Enums.Orders;

namespace FastKala.Application.ViewModels.Orders;

public class CheckoutViewModel
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string Town { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public ShippingMethods ShippingMethod { get; set; }
    public PaymentMethods PaymentMethod { get; set; }
    public long TotalPrice { get; set; }
    public bool AcceptTerms { get; set; }
}