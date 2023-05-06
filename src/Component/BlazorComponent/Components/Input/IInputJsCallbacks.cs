namespace BlazorComponent;

public interface IInputJsCallbacks
{
    Task HandleOnInputAsync(ChangeEventArgs args);

    Task HandleOnClickAsync(ExMouseEventArgs args);

    Task HandleOnMouseUpAsync(ExMouseEventArgs args);

    void StateHasChangedForJsInvokable();
}
