using System.Reflection;

namespace BlazorComponent;

public static class ComponentExtensions
{
    public static IDictionary<string, object?> ToParameters(this ComponentBase component)
    {
        return ToParameters(component, p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(ParameterAttribute)));
    }

    public static IDictionary<string, object?> ToParameters(this object obj, Func<PropertyInfo, bool>? condition = null)
    {
        var result = new Dictionary<string, object?>();

        var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

        if (condition is not null)
        {
            properties = properties.Where(condition).ToArray();
        }

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);
            if (value is null) continue;
            result.Add(property.Name, value);
        }

        return result;
    }
}
