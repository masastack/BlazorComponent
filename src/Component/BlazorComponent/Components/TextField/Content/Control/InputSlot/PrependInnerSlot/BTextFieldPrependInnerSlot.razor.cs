namespace BlazorComponent;

public partial class BTextFieldPrependInnerSlot<TValue, TInput> where TInput : ITextField<TValue>
{
    public RenderFragment? PrependInnerContent => Component.PrependInnerContent;

    public string? PrependInnerIcon => Component.PrependInnerIcon;

    public EventCallback<MouseEventArgs> HandleOnPrependInnerClickAsync =>
        Component.OnPrependInnerClick.HasDelegate
            ? EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnPrependInnerClickAsync)
            : default;

    public Action<ElementReference> PrependInnerReferenceCapture => element => Component.PrependInnerElement = element;
}
