using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Products;

namespace FastKala.Application.Interfaces;
public interface IProductService
{
    Task<ProductListViewModel> GetAllProducts(int count = 20);
    Task<ProductViewModel> GetProductById(int id);
    Task<OperationResult> UpdateProduct(ProductViewModel product);
    Task<OperationResult> RemoveProductById(int id);
}