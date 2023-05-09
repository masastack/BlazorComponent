namespace BlazorComponent;

public partial class BTextFieldIconSlot<TValue, TInput> where TInput : ITextField<TValue>
{
    public string? AppendIcon => Component.AppendIcon;

    public RenderFragment? AppendContent => Component.AppendContent;

    public EventCallback<MouseEventArgs> HandleOnAppendClickAsync => Component.OnAppendClick.HasDelegate
        ? EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnAppendClickAsync)
        : default;

    public Action<ElementReference> AppendInnerReferenceCapture => element => Component.AppendInnerElement = element;
}