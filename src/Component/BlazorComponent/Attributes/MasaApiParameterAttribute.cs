namespace BlazorComponent.Attributes;

public class MassApiParameterAttribute : Attribute
{
    public object? DefaultValue { get; }

    public bool Ignored { get; set; }

    public string? ReleasedOn { get; set; }

    public MassApiParameterAttribute(object defaultValue)
    {
        DefaultValue = defaultValue;
    }

    public MassApiParameterAttribute(object defaultValue, string releasedOn)
    {
        DefaultValue = defaultValue;
        ReleasedOn = releasedOn;
    }

    public MassApiParameterAttribute()
    {
    }
}
