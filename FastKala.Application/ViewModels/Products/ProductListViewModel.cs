using FastKala.Domain.Models;

namespace FastKala.Application.ViewModels.Products;
public class ProductListViewModel
{
    public List<Product> Products { get; set; } = new List<Product>();
    public int TotalProductsCount { get; set; }
}
