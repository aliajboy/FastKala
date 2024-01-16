using FastKala.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastKala.Domain.Models;

public record Product
{
    public int Id { get; set; }
    [DisplayName("نام کالا")]
    public string Name { get; set; } = "پیش فرض";
    [DisplayName("عنوان انگلیسی")]
    public string? EnglishName { get; set; }
    [DisplayName("کد محصول")]
    public int? Sku { get; set; }
    [DisplayName("توضیحات")]
    public string? Description { get; set; } = "";
    [DisplayName("برند")]
    public int BrandId { get; set; }
    [DisplayName("قیمت")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public int Price { get; set; }
    [DisplayName("قیمت فروش ویژه")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public int? SalePrice { get; set; }
    [DisplayName("موجودی")]
    public int? StockQuantity { get; set; }
    [DisplayName("مدیریت موجودی انبار")]
    public bool ManageStockQuantity { get; set; }
    [DisplayName("مدیریت تعداد فروش")]
    public bool ManageSaleQuantity { get; set; }
    [DisplayName("حداقل تعداد فروش")]
    public int? MinimumSaleQuantity { get; set; }
    [DisplayName("گام تعداد فروش")]
    public int? SaleQuantityStep { get; set; }
    [DisplayName("وزن")]
    public int? Weight { get; set; }
    [DisplayName("وضعیت انتشار")]
    public ProductStatus Status { get; set; }
    [DisplayName("تصویر اصلی")]
    public string? MainImage { get; set; }
    [DisplayName("دسته بندی اصلی")]
    public int MainCategoryId { get; set; } = 1;
    [DisplayName("دسته بندی اصلی")]
    public DateTime LastChangeTime { get; set; } = DateTime.Now;
    [DisplayName("امتیاز محصول")]
    public byte Rate { get; set; } = 0;

    // Relations

    [DisplayName("ویژگی های محصول")]
    public List<ProductFeature> ProductFeatures { get; set; } = new List<ProductFeature>();

    [DisplayName("نقاط قوت و ضعف")]
    public List<ProductProsCons> ProductProsCons { get; set; } = new();
    public List<ProductAttributeValues> AttributeValues { get; set; } = new();

    public List<ProductAttributeRelation> AttributesRelations { get; set; } = new();

    public List<ProductCategoryRelation> CategoriesRelations { get; set; } = new();

    public List<ProductTagRelation> TagsRelations { get; set; } = new();
}