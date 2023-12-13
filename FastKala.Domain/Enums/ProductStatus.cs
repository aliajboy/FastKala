using System.ComponentModel.DataAnnotations;

namespace FastKala.Domain.Enums;
public enum ProductStatus
{
    [Display(Name = "جدید")]
    New = 0,
    [Display(Name = "حذف شده")]
    Deleted = 1,
    [Display(Name = "پیش نویس")]
    Draft = 2,
    [Display(Name = "منتشر شده")]
    Published = 3
}