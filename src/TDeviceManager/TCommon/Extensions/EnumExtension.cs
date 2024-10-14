using System.ComponentModel;
using System.Reflection;

namespace TCommon.Extensions;

public static class EnumExtension
{
    public static T GetAttribute<T>(this Enum enumValue, bool isInherit = false) where T : Attribute
    {
        var type = enumValue.GetType();
        //获取到:Admin
        var enumName = Enum.GetName(type, enumValue);
        ArgumentNullException.ThrowIfNull(enumName);
        var field = type.GetField(enumName);
        ArgumentNullException.ThrowIfNull(field);
        var customAttribute = field.GetCustomAttribute(typeof(T), isInherit);
        ArgumentNullException.ThrowIfNull(customAttribute);
        return (customAttribute as T)!;
    }
}