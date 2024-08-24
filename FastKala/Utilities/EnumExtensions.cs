using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FastKala.Utilities;

public static class EnumExtensions
{
    public static string ToDisplay(this Enum enumValue)
    {
        return enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>()?.Name ?? "";
    }
}