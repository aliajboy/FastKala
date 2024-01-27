using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Products;
using FastKala.Domain.Enums;
using FastKala.Domain.Models.Product;

namespace FastKala.Application.Interfaces.Product;
public interface IProductService
{
    #region Product

    Task<ProductListViewModel> GetAllProducts(ProductStatus status = ProductStatus.All, int count = 20);
    Task<ProductViewModel> GetProductById(int id);
    Task<OperationResult> UpdateProduct(ProductViewModel product);
    Task<OperationResult> RemoveProductById(int id);
    Task<OperationResult> AddProduct(ProductViewModel product);

    #endregion

    #region Attribute

    Task<OperationResult> RemoveAttributeById(int id);
    Task<ProductAtrributesListViewModel> GetAllProductAttributes();
    Task<ProductAttributeValueViewModel> GetProductAttributeById(int id);
    Task<OperationResult> AddProductAttribute(string attributeName, string attributeLink, int attributeType);
    Task<OperationResult> UpdateAttribute(int id, string name, string link, byte type);

    #endregion

    #region Attribute Values

    Task<OperationResult> AddAttributeValue(string attributeValueName, string attributeValeLink, int attributeId);
    /// <summary>
    /// Get Attribute Values by ProductAttributeId and Name Content
    /// </summary>
    /// <param name="id">ProductAttributeId</param>
    /// <param name="content">search content for Name of Attribute Value</param>
    /// <returns>List of Attribute Values that has Id of "id" and Their Names contains "content"</returns>
    Task<AttributeValuesResponse> GetAttributeValues(int id, string content);
    /// <summary>
    /// Get Attribute Value By its Id
    /// </summary>
    /// <param name="attributeId">Attribute Id</param>
    /// <returns>Attribute Value Or Null if not existed in database</returns>
    Task<ProductAttributeValueViewModel> GetAttributeValue(int attributeId);
    /// <summary>
    /// Update Attribute Value
    /// </summary>
    /// <param name="id">Attribute Id</param>
    /// <param name="name">Attribute Name</param>
    /// <param name="value">Attribute Value</param>
    /// <returns>Operation Result</returns>
    Task<OperationResult> UpdateAttributeValue(int id, string name, string value);
    Task<OperationResult> RemoveAttributeValue(int id);

    #endregion

    #region Categories

    /// <summary>
    /// Get Product Category if Id is Filled Or Get List of All Categories if Id is null or 0.
    /// </summary>
    /// <param name="id">Category Id</param>
    /// <returns>List of All Categories if Id is NULL or 0.
    /// Or returns a Category if Id is Filled.</returns>
    Task<ProductCategoriesViewModel> GetProductCategories(int id = 0);
    Task<OperationResult> AddProductCategory(ProductCategoriesViewModel category);
    Task<OperationResult> RemoveProductCategory(int id);
    Task<OperationResult> UpdateProductCategory(ProductCategoriesViewModel category);

    #endregion

    #region Tags

    Task<ProductTagsViewModel> GetProductTags(int id = 0);
    Task<OperationResult> AddProductTag(string name, string link, string description);
    Task<OperationResult> RemoveProductTag(int id);
    Task<OperationResult> UpdateProductTag(int id, string name, string link, string description);

    #endregion

    #region Brands

    Task<ProductBrandsViewModel> GetProductBrands(int id = 0);

    Task<OperationResult> AddProductBrand(string name, string link, string description = "");

    Task<OperationResult> RemoveProductBrand(int id);

    Task<OperationResult> UpdateProductBrand(int id, string name, string link, string description = "");

    #endregion

    #region Product Comment

    Task<ProductCommentListViewModel> GetProductComment(int id);

    Task<OperationResult> AddProductComment(ProductCommentViewModel productCommentViewModel);

    Task<OperationResult> RemoveProductComment();

    Task<OperationResult> UpdateProductComment(ProductCommentViewModel productCommentView);

    Task<OperationResult> UpdateProductComment(CommentStatus status);

    #endregion
}