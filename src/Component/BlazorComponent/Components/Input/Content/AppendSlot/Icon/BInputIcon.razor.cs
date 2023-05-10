namespace BlazorComponent;

public partial class BInputIcon<TValue, TInput> : ComponentPartBase<TInput>
    where TInput : IInput<TValue>
{
    [Parameter]
    public string? Icon { get; set; }

    [Parameter, EditorRequired]
    public string Type { get; set; } = null!;

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter]
    public Action<ElementReference>? ReferenceCapture { get; set; }

    private ElementReference Element
    {
        set => ReferenceCapture?.Invoke(value);
    }
}
