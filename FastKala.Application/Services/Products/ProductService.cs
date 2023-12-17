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

    public async Task<OperationResult> AddAttributeValue(string attributeValueName, string attributeValeLink, int attributeId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection.ExecuteAsync("INSERT INTO ProductAttributeValues ([Name],[value],[ProductAttributeId]) VALUES (@name,@value,@productAttributeId)",
                    new { name = attributeValueName, value = attributeValeLink, productAttributeId = attributeId });
                if (result == 1)
                {
                    return new OperationResult() { OperationStatus = OperationStatus.Success, Message = "ویژگی محصول باموفقیت اضافه شد" };
                }
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail, Message = "خطا در دیتابیس" };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception, Message = "خطای سیستمی" };
        }
    }

    public async Task<OperationResult> AddProduct(ProductViewModel product)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Add Product
                int insertedId = await connection.QuerySingleAsync<int>("INSERT INTO Products ([Name],[Description],[Price],[SalePrice],[StockQuantity],[Sku],[ManageSaleQuantity],[ManageStockQuantity],[MinimumSaleQuantity],[SaleQuantityStep],[Weight],[EnglishName],[Status],[MainImage]) output INSERTED.Id VALUES (@name,@description,@price,@salePrice,@stockQuantity,@sku,@manageSaleQuantity,@manageStockQuantity,@minimumSaleQuantity,@saleQuantity,@weight,@englishName,@status,@mainImage)", new
                {
                    name = product.Product.Name,
                    description = product.Product.Description,
                    price = product.Product.Price,
                    salePrice = product.Product.SalePrice,
                    stockQuantity = product.Product.StockQuantity,
                    sku = product.Product.Sku,
                    manageSaleQuantity = product.Product.ManageSaleQuantity,
                    manageStockQuantity = product.Product.ManageStockQuantity,
                    minimumSaleQuantity = product.Product.MinimumSaleQuantity,
                    saleQuantity = product.Product.SaleQuantityStep,
                    weight = product.Product.Weight,
                    englishName = product.Product.EnglishName,
                    status = product.Product.Status,
                    mainImage = product.Product.MainImage
                });

                // Add Product Features
                foreach (var item in product.Product.ProductFeatures)
                {
                    await connection.ExecuteAsync("INSERT INTO ProductFeature ([TitleName],[Value],[ProductId]) VALUES (@titleName,@value,@productId)",
                    new { titleName = item.TitleName, value = item.Value, productId = insertedId });
                }

                foreach (var item in product.ProductPros)
                {
                    if (product.ProductPros.FirstOrDefault() != null)
                    {
                        product.Product.ProductProsCons.Add(new ProductProsCons() { Text = item, IsPros = ProsConsType.Pros, ProductId = insertedId });
                    }
                }

                foreach (var item in product.ProductCons)
                {
                    if (product.ProductCons.FirstOrDefault() != null)
                    {
                        product.Product.ProductProsCons.Add(new ProductProsCons() { Text = item, IsPros = ProsConsType.Cons, ProductId = insertedId });
                    }
                }

                if (product.Product.ProductProsCons != null)
                {
                    foreach (var item in product.Product.ProductProsCons)
                    {
                        await connection.ExecuteAsync("INSERT INTO [dbo].[ProductProsCons] ([Text],[IsPros],[ProductId]) VALUES (@text,@isPros,@productId)",
                        new { text = item.Text, isPros = item.IsPros, productId = insertedId });
                    }
                }

                if (product.Product.Attributes.Count > 0)
                {
                    foreach (var item in product.Product.Attributes)
                    {
                        await connection.ExecuteAsync("INSERT INTO ProductAttributeRelations ([ProductId],[AttributeValueId]) VALUES (@productId,@attributeValueId)",
                        new { productId = insertedId, attributeValueId = item.AttributeValueId });
                    }
                }

            }

            return new OperationResult() { OperationStatus = OperationStatus.Success, Message = "محصول با موفقیت حذف شد" };
        }
        catch (Exception ex)
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception, Message = ex.Message };
        }
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

    public async Task<ProductAttributeValueViewModel> GetProductAttributeById(int id)
    {
        ProductAttributeValueViewModel productAtrribute = new();
        try
        {
            using (SqlConnection connection = new(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync("SELECT * FROM ProductAttributes Where Id = @ID SELECT * From ProductAttributeValues where ProductAttributeId = @ID", new { ID = id }))
                {
                    productAtrribute.Attribute = await multi.ReadSingleAsync<ProductAttribute>();
                    productAtrribute.AttributeValues = multi.ReadAsync<ProductAttributeValues>().Result.ToList();
                }
            }
            return productAtrribute;
        }
        catch
        {
            return productAtrribute;
        }
    }

    public async Task<AttributeValuesResponse> GetAttributeValuesByIdAjax(int id, string content)
    {
        List<ProductAttributeValues> productAtrribute = new();
        try
        {
            using (SqlConnection connection = new(_connectionString))
            {
                var values = await connection.QueryAsync<ProductAttributeValues>("Select * From ProductAttributeValues where ProductAttributeId = @Id and Name Like N'%" + content + "%'", new { Id = id });
                productAtrribute = values.ToList();
            }
            return new AttributeValuesResponse() { results = productAtrribute };
        }
        catch
        {
            return new AttributeValuesResponse();
        }
    }

    public async Task<ProductViewModel> GetProductById(int id)
    {
        ProductViewModel product = new();
        try
        {
            using (SqlConnection connection = new(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync("SELECT * FROM Products Where Id = @ID SELECT * FROM ProductFeature Where ProductId = @ID SELECT * FROM ProductProsCons Where ProductId = @ID", new { ID = id }))
                {
                    product.Product = await multi.ReadSingleAsync<Product>();
                    product.Product.ProductFeatures = multi.ReadAsync<ProductFeature>().Result.ToList();
                    product.Product.ProductProsCons = multi.ReadAsync<ProductProsCons>().Result.ToList();
                }
            }
            return product;
        }
        catch
        {
            return product;
        }
    }

    public async Task<OperationResult> RemoveProductById(int id)
    {
        int res;
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                res = await connection.ExecuteAsync("Delete From Products where Id = @ID", new { ID = id });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success, Message = "محصول با موفقیت حذف شد" };
            }


            return new OperationResult() { OperationStatus = OperationStatus.Fail, Message = "محصولی حذف نشد یا بیش از یک محصول حذف شده است" };
        }
        catch (Exception ex)
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception, Message = ex.Message };
        }
    }

    public Task<OperationResult> UpdateProduct(ProductViewModel product)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {

        }
        throw new NotImplementedException();
    }

    public Task<OperationResult> RemoveAttributeById(int id)
    {
        throw new NotImplementedException();
    }
}