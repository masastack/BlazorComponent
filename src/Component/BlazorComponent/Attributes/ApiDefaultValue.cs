namespace BlazorComponent.Attributes;

public class ApiDefaultValueAttribute : Attribute
{
    public object Value { get; }

    public ApiDefaultValueAttribute(object value)
    {
        Value = value;
    }
}

