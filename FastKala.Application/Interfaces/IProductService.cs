﻿using FastKala.Application.ViewModels.Global;
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
    Task<OperationResult> UpdateAttribute(int id, string name, string link, byte type);
}