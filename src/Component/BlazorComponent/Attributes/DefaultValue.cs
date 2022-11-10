namespace BlazorComponent.Attributes;

public class DefaultValue : Attribute
{
    public object Value { get; }

    public DefaultValue(object value)
    {
        Value = value;
    }
}

