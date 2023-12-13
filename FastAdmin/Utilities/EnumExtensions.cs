using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FastAdmin.Utilities;

public static class EnumExtensions
{
    public static string ToDisplay(this Enum enumValue)
    {
        return enumValue.GetType().GetMember(enumValue.ToString()).First()?.GetCustomAttribute<DisplayAttribute>()?.Name;
    }
}