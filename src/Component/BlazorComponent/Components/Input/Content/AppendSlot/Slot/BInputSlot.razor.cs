namespace BlazorComponent;

public partial class BInputSlot<TValue, TInput> : ComponentPartBase<TInput>
    where TInput : IInput<TValue>
{
    [Parameter]
    public string Type { get; set; }

    [Parameter]
    public string Location { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public Action<ElementReference> ReferenceCapture { get; set; }

    public ElementReference Element
    {
        set
        {
            ReferenceCapture?.Invoke(value);
        }
    }
}