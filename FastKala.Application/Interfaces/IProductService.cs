using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Products;

namespace FastKala.Application.Interfaces;
public interface IProductService
{
    Task<ProductListViewModel> GetAllProducts(int count = 20);
    Task<ProductViewModel> GetProductById(int id);
    Task<OperationResult> UpdateProduct(ProductViewModel product);
    Task<OperationResult> RemoveProductById(int id);
    Task<OperationResult> RemoveAttributeById(int id);
    Task<ProductAtrributesListViewModel> GetAllProductAttributes();
    Task<ProductAttributeValueViewModel> GetProductAttributeById(int id);
    Task<OperationResult> AddProduct(ProductViewModel product);
    Task<OperationResult> AddProductAttribute(string attributeName, string attributeLink, int attributeType);
    Task<OperationResult> AddAttributeValue(string attributeValueName, string attributeValeLink, int attributeId);
    Task<AttributeValuesResponse> GetAttributeValuesByIdAjax(int id, string content);
}