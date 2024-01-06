using System.ComponentModel.DataAnnotations;

namespace FastKala.Application.ViewModels.Global;
public enum ImageType
{
    [Display(Name = "تصویر محصولات")]
    ProductImages = 1,
    [Display(Name = "تصویر پروفایل")]
    ProfileImages = 2
}