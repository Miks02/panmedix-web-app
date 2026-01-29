using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PanMedix.Helpers;

public static class EnumUtility
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())[0]
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName() ?? enumValue.ToString();
    }

    public static List<string> GetDisplayNames<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e =>
            {
                var display = e.GetType()
                    .GetField(e.ToString())
                    ?.GetCustomAttribute<DisplayAttribute>();

                return display?.Name ?? e.ToString();
            })
            .ToList();
    }
    
    public static IEnumerable<SelectListItem> GetEnumSelectListExclude<TEnum>(this IHtmlHelper html, params TEnum[] excludeValues) where TEnum : struct
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Where(x => !excludeValues.Contains(x))
            .Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.GetType()
                    .GetMember(e.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    ?.GetName() ?? e.ToString()
            });
    }
}