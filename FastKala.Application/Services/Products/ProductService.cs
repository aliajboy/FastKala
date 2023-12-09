using Dapper;
using FastKala.Application.Interfaces;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Products;
using FastKala.Domain.Enums;
using FastKala.Domain.Models;
using Microsoft.Data.SqlClient;

namespace FastKala.Application.Services.Products;
public class ProductService : IProductService
{
    private readonly string _connectionString;
    public ProductService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<OperationResult> AddProduct(ProductViewModel product)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult> AddProductAttribute(string attributeName, string attributeLink, int attributeType)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection.ExecuteAsync("INSERT INTO ProductAttributes (Name,Link,Type) VALUES (@name,@link,@type)",
                    new { name = attributeName, link = attributeLink, type = attributeType });
                if (result == 1)
                {
                    return new OperationResult() { OperationStatus = OperationStatus.Exception, Message = "ویژگی محصول باموفقیت اضافه شد" };
                }
            }
            return new OperationResult() { OperationStatus = OperationStatus.Exception, Message = "خطا در دیتابیس" };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception, Message = "خطای سیستمی" };
        }
    }

    public async Task<ProductAtrributesListViewModel> GetAllProductAttributes()
    {
        ProductAtrributesListViewModel productAtrributesList = new ProductAtrributesListViewModel();
        try
        {
            using (SqlConnection connection = new(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync("SELECT * FROM ProductAttributes SELECT * FROM ProductAttributeValues"))
                {
                    var attributes = await multi.ReadAsync<ProductAttribute>();
                    var attributesValues = await multi.ReadAsync<ProductAttributeValues>();
                    productAtrributesList.ProductAttributes = attributes.ToList();
                    productAtrributesList.ProductAttributeValues = attributesValues.ToList();
                }
            }
            return productAtrributesList;
        }
        catch
        {
            return productAtrributesList;
        }
    }

    public async Task<ProductListViewModel> GetAllProducts(int count = 20)
    {
        ProductListViewModel productList = new();
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var products = await connection.QueryAsync<Product>("SELECT TOP (@Count) * FROM Products", new { Count = count });
                productList.TotalProductsCount = await connection.QuerySingleOrDefaultAsync<int>("SELECT COUNT(Id) FROM Products");
                productList.Products = products.ToList();
            }
            return productList;
        }
        catch
        {
            return productList;
        }
    }

    public Task<ProductAttributeViewModel> GetProductAttributeById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> GetProductById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {

        }
        throw new NotImplementedException();
    }

    public Task<OperationResult> RemoveProductById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {

        }
        throw new NotImplementedException();
    }

    public Task<OperationResult> UpdateProduct(ProductViewModel product)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {

        }
        throw new NotImplementedException();
    }
}