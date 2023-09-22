namespace BlazorComponent;

public interface IResizeJSModule
{
    ValueTask ObserverAsync(ElementReference el, Func<Task> handle);

    ValueTask UnobserveAsync(ElementReference el);
}
