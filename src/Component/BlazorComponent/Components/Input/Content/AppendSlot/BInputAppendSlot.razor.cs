namespace BlazorComponent;

public partial class BInputAppendSlot<TValue, TInput> where TInput : IInput<TValue>
{
    public string? AppendIcon => Component.AppendIcon;

    public RenderFragment? AppendContent => Component.AppendContent;

    public EventCallback<MouseEventArgs> HandleOnAppendClickAsync =>
        Component.OnAppendClick.HasDelegate
            ? EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnAppendClickAsync)
            : default;
}
