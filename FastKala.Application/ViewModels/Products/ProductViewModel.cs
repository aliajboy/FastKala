using FastKala.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace FastKala.Application.ViewModels.Products;
public class ProductViewModel
{
    public Product Product { get; set; } = new Product();

    public List<string> ProductPros { get; set; } = new();
    public List<string> ProductCons { get; set; } = new();
    public List<ProductAttribute> ProductAttributes { get; set; } = new();
    public ProductCategory MainCategory { get; set; } = new();
    public List<ProductCategory> Categories { get; set; } = new();
    public List<ProductCategory> CategoryOrder { get; set; } = new();
    public ProductBrand? Brand { get; set; }
    public List<ProductBrand> Brands { get; set; } = new();
    public List<ProductTag> Tags { get; set; } = new();
    public IFormFile? MainImage { get; set; }
    public List<IFormFile>? GalleryImages { get; set; }
    public List<ProductImage> ProductImages { get; set; } = new();
}