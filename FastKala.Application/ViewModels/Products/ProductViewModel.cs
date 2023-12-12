using FastKala.Domain.Models;

namespace FastKala.Application.ViewModels.Products;
public class ProductViewModel
{
    public Product Product { get; set; } = new Product();

    public List<string> ProductPros { get; set; } = new();
    public List<string> ProductCons { get; set; } = new();
}