using System.ComponentModel.DataAnnotations;

namespace FastKala.Domain.Enums;
public enum AttributeType : byte
{
    [Display(Name = "ساده")]
    Simple = 1,
    [Display(Name = "رنگ")]
    Color = 2,
    [Display(Name = "انتخابی")]
    Select = 3,
}