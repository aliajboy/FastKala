﻿using Dapper;
using FastKala.Application.Data;
using FastKala.Application.Interfaces.Global;
using FastKala.Application.Interfaces.Product;
using FastKala.Application.ViewModels.Global;
using FastKala.Application.ViewModels.Products;
using FastKala.Domain.Enums;
using FastKala.Domain.Models.Product;
using Microsoft.Data.SqlClient;
using System.Data;
using Z.Dapper.Plus;

namespace FastKala.Application.Services.Products;
public class ProductService : IProductService
{
    private readonly DapperContext _context;
    private readonly IUploadService _uploadService;
    public ProductService(IUploadService uploadService, DapperContext context)
    {
        _uploadService = uploadService;
        _context = context;
    }

    #region Product

    public async Task<OperationResult> AddProduct(ProductViewModel product)
    {
        try
        {
            string mainImageURL = "";
            if (product.MainImage != null)
            {
                var result = await _uploadService.UploadSingleImages(product.MainImage, ImageType.ProductImages, ImageSize.TwoMegabyte);
                mainImageURL = result.Message ?? "";
            }

            using (SqlConnection connection = _context.CreateConnection())
            {
                await connection.OpenAsync();

                // Add Product
                int insertedId = await connection.ExecuteScalarAsync<int>("INSERT INTO Products (Name,Status,Description,BrandId,Price,SalePrice,StockQuantity,Sku,ManageSaleQuantity,ManageStockQuantity,MinimumSaleQuantity,SaleQuantityStep,Weight,EnglishName,MainImage,LastChangeTime,MainCategoryId) OUTPUT Inserted.ID VALUES  (@name,@status,@description,@brandId,@price,@salePrice,@stockQuantity,@sku,@manageSaleQuantity,@manageStockQuantity,@minimumSaleQuantity,@saleQuantityStep,@weight,@englishName,@mainImage,@lastChangeTime,@mainCategoryId)", new
                {
                    name = product.Product.Name,
                    status = ProductStatus.Published,
                    description = product.Product.Description,
                    brandId = product.Product.BrandId,
                    price = product.Product.Price,
                    salePrice = product.Product.SalePrice,
                    stockQuantity = product.Product.StockQuantity,
                    sku = product.Product.Sku,
                    manageSaleQuantity = product.Product.ManageSaleQuantity,
                    manageStockQuantity = product.Product.ManageStockQuantity,
                    minimumSaleQuantity = product.Product.MinimumSaleQuantity,
                    saleQuantityStep = product.Product.SaleQuantityStep,
                    weight = product.Product.Weight,
                    englishName = product.Product.EnglishName,
                    mainImage = mainImageURL,
                    lastChangeTime = DateTime.Now,
                    mainCategoryId = product.Product.MainCategoryId
                });

                List<ProductImage> galleryImages = new();
                if (product.GalleryImages?.Count > 0)
                {
                    galleryImages = await _uploadService.UploadMultipleImages(product.GalleryImages, ImageType.ProductImages, ImageSize.TwoMegabyte, insertedId);
                }

                // Add Product Features
                foreach (var item in product.Product.ProductFeatures)
                {
                    item.ProductId = insertedId;
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
                foreach (var item in product.Product.AttributesRelations)
                {
                    item.ProductId = insertedId;
                }
                foreach (var item in product.Product.CategoriesRelations)
                {
                    item.ProductId = insertedId;
                }
                foreach (var item in product.Product.TagsRelations)
                {
                    item.ProductId = insertedId;
                }


                connection.BulkInsert<ProductImage>(galleryImages);
                connection.BulkInsert<ProductTagRelation>(product.Product.TagsRelations);
                connection.BulkInsert<ProductCategoryRelation>(product.Product.CategoriesRelations);
                connection.BulkInsert<ProductFeature>(product.Product.ProductFeatures);
                connection.BulkInsert<ProductProsCons>(product.Product.ProductProsCons);
                connection.BulkInsert<ProductAttributeRelation>(product.Product.AttributesRelations);

                await connection.CloseAsync();
            }

            return new OperationResult() { OperationStatus = OperationStatus.Success, Message = "محصول با موفقیت حذف شد" };
        }
        catch (Exception ex)
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception, Message = ex.Message };
        }
    }

    public async Task<ProductListViewModel> GetAllProducts(ProductStatus status = ProductStatus.All, int count = 20)
    {
        ProductListViewModel productList = new();
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                IEnumerable<Product> products;
                if (status == ProductStatus.All)
                {
                    products = await connection.QueryAsync<Product>("SELECT TOP 20 * FROM Products");
                }
                else
                {
                    products = await connection.QueryAsync<Product>("SELECT TOP (@Count) * FROM Products where Status = @productStatus", new { Count = count, productStatus = status });
                }
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

    public async Task<ProductViewModel> GetProductById(int id)
    {
        ProductViewModel product = new();
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync("EXEC GetProduct @productId = @ID", new { ID = id }))
                {
                    product.Product = await multi.ReadSingleAsync<Product>();
                    product.Product.ProductFeatures = multi.ReadAsync<ProductFeature>().Result.ToList();
                    product.Product.ProductProsCons = multi.ReadAsync<ProductProsCons>().Result.ToList();
                    product.Product.AttributesRelations = multi.ReadAsync<ProductAttributeRelation>().Result.ToList();
                    product.Product.CategoriesRelations = multi.ReadAsync<ProductCategoryRelation>().Result.ToList();
                    product.Product.TagsRelations = multi.ReadAsync<ProductTagRelation>().Result.ToList();
                    product.ProductImages = multi.ReadAsync<ProductImage>().Result.ToList();
                    product.Categories = multi.ReadAsync<ProductCategory>().Result.ToList();
                    product.Tags = multi.ReadAsync<ProductTag>().Result.ToList();
                }
                product.Brand = await connection.QuerySingleAsync<ProductBrand>("SELECT Name ,Link FROM ProductBrands where Id = @brandId", new { brandId = product.Product.BrandId });
                string sql = "select pa.Id,pa.Name,pa.Link,pa.Type,pav.Id,pav.Name,pav.value,pav.ProductAttributeId from productattributes pa inner join productattributevalues pav on pav.ProductattributeId = pa.Id inner join ProductAttributeRelations par on par.AttributeValueId = pav.Id where ProductId = @Id";

                var attributeResult = await connection.QueryAsync<ProductAttribute, ProductAttributeValues, ProductAttribute>(sql, (attributes, attributevalues) =>
                {
                    attributes.AttributeValues.Add(attributevalues);
                    return attributes;
                }, splitOn: "Id", param: new {Id = id});

                product.ProductAttributes = attributeResult.GroupBy(p => p.Id).Select(g =>
                {
                    var productAttribute = g.First();
                    productAttribute.AttributeValues = g.Select(p => p.AttributeValues.Single()).ToList();
                    return productAttribute;
                }).ToList();
            }

            // Make Categories in Order
            if (product.Categories.Any())
            {
                product.MainCategory = product.Categories.First(x => x.Id == product.Product.MainCategoryId);

                product.CategoryOrder.Add(product.MainCategory);
                int parentId = product.MainCategory.ParentId;
                while (parentId != 0)
                {
                    var pCategory = product.Categories.First(x => x.Id == parentId);
                    product.CategoryOrder.Add(pCategory);
                    parentId = pCategory.ParentId;
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
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync("RemoveProduct", new { productId = id }, commandType: CommandType.StoredProcedure);
            }

            return new OperationResult() { OperationStatus = OperationStatus.Success, Message = "محصول با موفقیت حذف شد" };
        }
        catch (Exception ex)
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception, Message = ex.Message };
        }
    }

    public async Task<OperationResult> UpdateProduct(ProductViewModel product)
    {
        //using (SqlConnection connection = _context.CreateConnection())
        //{

        //}
        throw new NotImplementedException();
    }

    #endregion

    #region Attributes

    public async Task<OperationResult> AddProductAttribute(string attributeName, string attributeLink, int attributeType)
    {
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
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
            using (SqlConnection connection = _context.CreateConnection())
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

    public async Task<ProductAttributeValueViewModel> GetProductAttributeById(int id)
    {
        ProductAttributeValueViewModel productAtrribute = new();
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
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

    public async Task<OperationResult> RemoveAttributeById(int id)
    {
        try
        {
            int res = 0;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("RemoveProductAttribute", new { AttributeId = id }, commandType: System.Data.CommandType.StoredProcedure);
            }
            if (res >= 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    public async Task<OperationResult> UpdateAttribute(int id, string name, string link, byte type)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("Update ProductAttributes SET Name = @name, Link = @link, Type = @type Where Id = @ID",
                    new
                    {
                        id = id,
                        name = name,
                        link = link,
                        type = type
                    });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }

            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    #endregion

    #region Attribute Values

    public async Task<OperationResult> AddAttributeValue(string attributeValueName, string attributeValeLink, int attributeId)
    {
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync("INSERT INTO ProductAttributeValues (Name,value,ProductAttributeId) VALUES (@name,@value,@productAttributeId)",
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

    public async Task<AttributeValuesResponse> GetAttributeValues(int id, string content)
    {
        List<ProductAttributeValues> productAtrribute = new();
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
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

    public async Task<ProductAttributeValueViewModel> GetAttributeValue(int attributeId)
    {
        ProductAttributeValueViewModel attributeValue = new();
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                attributeValue.AttributeValue = await connection.QuerySingleOrDefaultAsync<ProductAttributeValues>("Select * From ProductAttributeValues where Id = @Id", new { Id = attributeId });
            }
            return attributeValue;
        }
        catch
        {
            return new ProductAttributeValueViewModel();
        }
    }

    public async Task<OperationResult> UpdateAttributeValue(int id, string name, string value)
    {
        OperationResult result = new();
        try
        {
            int res = 0;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("UPDATE ProductAttributeValues SET Name = @name, value = @value WHERE Id = @id", new
                {
                    name = name,
                    value = value,
                    id = id
                });
            }
            if (res == 1)
            {
                result.OperationStatus = OperationStatus.Success;
                return result;
            }

            result.OperationStatus = OperationStatus.Fail;
            return result;
        }
        catch
        {
            result.OperationStatus = OperationStatus.Exception;
            return result;
        }
    }

    public async Task<OperationResult> RemoveAttributeValue(int id)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("Delete From ProductAttributeValues where Id = @ID", new { ID = id });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    #endregion

    #region Categories

    public async Task<ProductCategoriesViewModel> GetProductCategories(int id = 0)
    {
        ProductCategoriesViewModel productCategories = new();
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                if (id == 0)
                {
                    var categories = await connection.QueryAsync<ProductCategory>("Select * From ProductCategories");
                    productCategories.Categories = categories.ToList();
                }
                else if (id > 0)
                {
                    productCategories.Category = await connection.QuerySingleOrDefaultAsync<ProductCategory>("Select TOP 1 * From ProductCategories where Id = @Id", new { Id = id });
                }
            }
            return productCategories;
        }
        catch
        {
            return productCategories;
        }
    }

    public async Task<OperationResult> AddProductCategory(ProductCategoriesViewModel category)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("INSERT INTO ProductCategories (Name,Link,Description,ParentId) VALUES (@name,@link,@description,@parentId)", new
                {
                    name = category.Category.Name,
                    link = category.Category.Link,
                    description = category.Category.Description,
                    parentId = category.Category.ParentId
                });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    public async Task<OperationResult> RemoveProductCategory(int id)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("Delete From ProductCategories Where Id = @ID", new
                {
                    ID = id
                });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    public async Task<OperationResult> UpdateProductCategory(ProductCategoriesViewModel category)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("UPDATE ProductCategories SET Name = @name,Link = @link,Description = @description,ParentId = @parentId WHERE Id = @id", new
                {
                    id = category.Category.Id,
                    name = category.Category.Name,
                    link = category.Category.Link,
                    description = category.Category.Description,
                    parentId = category.Category.ParentId
                });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    #endregion

    #region Tags

    public async Task<ProductTagsViewModel> GetProductTags(int id = 0)
    {
        ProductTagsViewModel productTags = new();
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                if (id == 0)
                {
                    var tags = await connection.QueryAsync<ProductTag>("Select * From ProductTags");
                    productTags.ProductTags = tags.ToList();
                }
                else if (id > 0)
                {
                    productTags.ProductTag = await connection.QuerySingleOrDefaultAsync<ProductTag>("Select TOP 1 * From ProductTags where Id = @Id", new { Id = id });
                }
            }
            return productTags;
        }
        catch
        {
            return productTags;
        }
    }

    public async Task<OperationResult> AddProductTag(string name, string link, string description)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("INSERT INTO ProductTags (Name,Link,Description) VALUES (@name,@link,@description)", new
                {
                    name = name,
                    link = link,
                    description = description ?? ""
                });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    public async Task<OperationResult> RemoveProductTag(int id)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("Delete From ProductTags Where Id = @ID", new
                {
                    ID = id
                });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    public async Task<OperationResult> UpdateProductTag(int id, string name, string link, string description)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("UPDATE ProductTags SET Name = @name,Link = @link,Description = @description WHERE Id = @id", new
                {
                    id = id,
                    name = name,
                    link = link,
                    description = description ?? ""
                });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    #endregion

    #region Brands

    public async Task<ProductBrandsViewModel> GetProductBrands(int id = 0)
    {

        ProductBrandsViewModel productBrands = new();
        try
        {
            using (SqlConnection connection = _context.CreateConnection())
            {
                if (id == 0)
                {
                    var brands = await connection.QueryAsync<ProductBrand>("Select * From ProductBrands");
                    productBrands.Brands = brands.ToList();
                }
                else if (id > 0)
                {
                    productBrands.Brand = await connection.QuerySingleOrDefaultAsync<ProductBrand>("Select TOP 1 * From ProductBrands where Id = @Id", new { Id = id });
                }
            }
            return productBrands;
        }
        catch
        {
            return productBrands;
        }
    }

    public async Task<OperationResult> AddProductBrand(string name, string link, string description)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("INSERT INTO ProductBrands (Name,Link,Description) VALUES (@name,@link,@description)", new
                {
                    name = name,
                    link = link,
                    description = description ?? ""
                });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    public async Task<OperationResult> RemoveProductBrand(int id)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("Delete From roductBrands Where Id = @ID", new
                {
                    ID = id
                });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    public async Task<OperationResult> UpdateProductBrand(int id, string name, string link, string description)
    {
        try
        {
            int res;
            using (SqlConnection connection = _context.CreateConnection())
            {
                res = await connection.ExecuteAsync("UPDATE ProductBrands SET Name = @name,Link = @link,Description = @description WHERE Id = @id", new
                {
                    id = id,
                    name = name,
                    link = link,
                    description = description ?? ""
                });
            }
            if (res == 1)
            {
                return new OperationResult() { OperationStatus = OperationStatus.Success };
            }
            return new OperationResult() { OperationStatus = OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    #endregion

    #region Comments

    public async Task<OperationResult> AddProductComment(ProductCommentViewModel productCommentViewModel)
    {
        try
        {
            foreach (var item in productCommentViewModel.Advantages)
            {
            }
            using (SqlConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync("INSERT INTO ProductComments (Title,Description,Rate,HelpedCount,NotHelpedCount,SubmitDate,UserName,IsBuyer,Recommended,Status,ProductId) VALUES (@title,@description,@rate,@helpedCount,@notHelpedCount,@submitDate,@userName,@isBuyer,@recommended,@status,@productId)",
                    new
                    {
                        title = productCommentViewModel.ProductComment.Title,
                        description = productCommentViewModel.ProductComment.Description,
                        rate = productCommentViewModel.ProductComment.Rate,
                        helpedCount = productCommentViewModel.ProductComment.HelpedCount,
                        notHelpedCount = productCommentViewModel.ProductComment.NotHelpedCount,
                        submitDate= productCommentViewModel.ProductComment.SubmitDate,
                        userName = productCommentViewModel.ProductComment.UserName,
                        isBuyer = productCommentViewModel.ProductComment.IsBuyer,
                        recommended = productCommentViewModel.ProductComment.Recommended,
                        status = productCommentViewModel.ProductComment.Status,
                        productId = productCommentViewModel.ProductComment.ProductId
                    });
            }

            return new OperationResult() { OperationStatus = OperationStatus.Success };
        }
        catch
        {
            return new OperationResult() { OperationStatus = OperationStatus.Exception };
        }
    }

    public Task<OperationResult> RemoveProductComment()
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> UpdateProductComment(ProductCommentViewModel productCommentView)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> UpdateProductComment(CommentStatus status)
    {
        throw new NotImplementedException();
    }

    #endregion
}