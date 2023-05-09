namespace BlazorComponent;

public partial class BInputSlot<TValue, TInput> : ComponentPartBase<TInput>
    where TInput : IInput<TValue>
{
    [Parameter, EditorRequired]
    public string Type { get; set; } = null!;

    [Parameter, EditorRequired]
    public string Location { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public Action<ElementReference>? ReferenceCapture { get; set; }

    public ElementReference Element
    {
        set => ReferenceCapture?.Invoke(value);
    }
}