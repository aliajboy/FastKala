using Dapper;
using FastKala.Application.Interfaces;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Products;
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
    public async Task<ProductListViewModel> GetAllProducts(int count = 20)
    {
        ProductListViewModel productList = new();
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var products = await connection.QueryAsync<Product>("SELECT TOP (@Count) * FROM Products", new {Count = count});
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