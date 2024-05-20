using System.ComponentModel.DataAnnotations;

namespace FastKala.Domain.Enums.Products;

public enum ProductCommentRatings : byte
{
    [Display(Name = "خیلی بد")]
    VeryBad = 1,
    [Display(Name = "بد")]
    Bad = 2,
    [Display(Name = "متوسط")]
    Normal = 3,
    [Display(Name = "خوب")]
    Good = 4,
    [Display(Name = "عالی")]
    Perfect = 5
}