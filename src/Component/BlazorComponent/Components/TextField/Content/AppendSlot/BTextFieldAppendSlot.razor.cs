namespace BlazorComponent;

public partial class BTextFieldAppendSlot<TValue>
{
    public string? AppendOuterIcon => Component.AppendOuterIcon;

    public RenderFragment? AppendOuterContent => Component.AppendOuterContent;

    public EventCallback<MouseEventArgs> HandleOnAppendOuterClickAsync =>
        Component.OnAppendOuterClick.HasDelegate
            ? EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnAppendOuterClickAsync)
            : default;
}
