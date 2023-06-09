﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastKala.Models;

public class Product
{
    public int Id { get; set; }
    [DisplayName("نام کالا")]
    public string Name { get; set; } = "پیش فرض";
    [DisplayName("عنوان انگلیسی")]
    public string? EnglishName { get; set; }
    [DisplayName("کد محصول")]
    public int? Sku { get; set; }
    [DisplayName("توضیحات")]
    public string? Description { get; set; }
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

    // Relations

    [DisplayName("ویژگی های محصول")]
    public List<ProductFeature>? ProductFeatures { get; set; } = new List<ProductFeature>();

    [DisplayName("نقاط قوت")]
    public List<ProductPros>? ProductPros { get; set; } = new List<ProductPros>();
    [DisplayName("نقاط ضعف")]
    public List<ProductCons>? ProductCons { get; set; } = new List<ProductCons>();
}