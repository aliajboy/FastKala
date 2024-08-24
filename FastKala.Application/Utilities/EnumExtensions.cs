using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FastKala.Application.Utilities;
public static class EnumExtensions
{
    public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
    {
        Assert.NotNull(value, nameof(value));

        var attribute = value.GetType().GetField(value.ToString())?.GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

        if (attribute == null)
            return value.ToString();

        var propValue = attribute.GetType().GetProperty(property.ToString())?.GetValue(attribute, null);
        return propValue?.ToString() ?? "";
    }
}

public enum DisplayProperty
{
    Description,
    GroupName,
    Name,
    Prompt,
    ShortName,
    Order
}

public static class Assert
{
    public static void NotNull<T>(T obj, string name, string? message = null)
        where T : class
    {
        if (obj is null)
            throw new ArgumentNullException($"{name} : {typeof(T)}", message);
    }

    public static void NotNull<T>(T? obj, string name, string? message = null)
        where T : struct
    {
        if (!obj.HasValue)
            throw new ArgumentNullException($"{name} : {typeof(T)}", message);

    }

    public static void NotEmpty<T>(T obj, string name, string? message = null, T? defaultValue = null)
        where T : class
    {
        if (obj == defaultValue
            || (obj is string str && string.IsNullOrWhiteSpace(str))
            || (obj is IEnumerable list && !list.Cast<object>().Any()))
        {
            throw new ArgumentException("Argument is empty : " + message, $"{name} : {typeof(T)}");
        }
    }
}