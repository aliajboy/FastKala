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
    Task<AttributeValuesResponse> GetAttributeValues(int id, string content);
    Task<ProductAttributeValueViewModel> GetAttributeValue(int attributeId);
    Task<OperationResult> UpdateAttributeValue(int id, string name, string value);
    Task<OperationResult> RemoveAttributeValue(int id);
    Task<OperationResult> UpdateAttribute(int id, string name, string link, byte type);
}