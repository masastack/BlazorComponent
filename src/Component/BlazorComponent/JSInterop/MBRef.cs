namespace BlazorComponent.JSInterop;

public class MBRef
{
    private readonly IJSRuntime _jsRuntime;

    public ElementReference Value { get; private set; }

    public MBRef(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public MBRef(IJSRuntime jsRuntime, ElementReference value)
    {
        _jsRuntime = jsRuntime;
        Value = value;
    }

    public ElementReference Ref
    {
        set => Value = value;
    }

    public ValueTask<T> InvokeAsync<T>(string identifier, params object[] args)
    {
        return _jsRuntime.InvokeAsync<T>(identifier, Value, args);
    }
    
}

public static class MbRefExtensions
{
    public static ValueTask<BoundingClientRect> GetBoundingClientRectAsync(this MBRef mbRef)
    {
        return mbRef.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect);
    }
}
