using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent;

// TODO：change name to PolyCascadeValue?
public class BCascadingValue<TValue> : IComponent
{
    [Parameter] [EditorRequired] public TValue? Value { get; set; }

    [Parameter] public string? Name { get; set; }

    [Parameter] public bool IsFixed { get; set; }

    [Parameter] [EditorRequired] public RenderFragment? ChildContent { get; set; }

    private Type? _cascadingValueType;
    private RenderHandle _renderHandle;

    public void Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (Value is null) return Task.CompletedTask;

        _cascadingValueType ??= typeof(CascadingValue<>).MakeGenericType(Value.GetType());

        _renderHandle.Render(Render);

        return Task.CompletedTask;
    }

    private void Render(RenderTreeBuilder builder)
    {
        builder.OpenComponent(0, _cascadingValueType!);
        builder.AddAttribute(1, nameof(Value), Value);
        if (!string.IsNullOrEmpty(Name))
        {
            builder.AddAttribute(2, nameof(Name), Name);
        }

        builder.AddAttribute(3, nameof(IsFixed), IsFixed);
        builder.AddAttribute(4, nameof(ChildContent), ChildContent);
        builder.CloseComponent();
    }
}