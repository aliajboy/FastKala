using System.ComponentModel.DataAnnotations;

namespace FastKala.Domain.Enums.Products;
public enum ProductStatus : byte
{
    [Display(Name = "جدید")]
    New = 0,
    [Display(Name = "حذف شده")]
    Deleted = 1,
    [Display(Name = "پیش نویس")]
    Draft = 2,
    [Display(Name = "منتشر شده")]
    Published = 3,
    [Display(Name = "همه")]
    All = 4
}

public enum CommentStatus : byte
{
    [Display(Name = "در انتظار تایید")]
    Pending = 0,
    [Display(Name = "تایید شده")]
    Verified = 1,
    [Display(Name = "رد شده")]
    Denied = 2
}