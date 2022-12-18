using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent;

public class InputJsInterop : IAsyncDisposable
{
    private readonly IInputJsCallbacks _owner;
    private readonly IJSRuntime _jsRuntime;

    private DotNetObjectReference<InputJsInterop>? _selfReference;
    private IJSObjectReference? _inputJsReference;

    private ElementReference? _inputElement;

    public InputJsInterop(IInputJsCallbacks owner, IJSRuntime jsRuntime)
    {
        _owner = owner;
        _jsRuntime = jsRuntime;
    }

    public async ValueTask InitializeAsync(ElementReference input, ElementReference inputSlot, int internalDebounceInterval)
    {
        _inputElement = input;

        _selfReference = DotNetObjectReference.Create(this);
        _inputJsReference = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/input.js");
        await _inputJsReference!.InvokeVoidAsync("registerInputEvents", input, inputSlot, _selfReference, internalDebounceInterval);
    }

    [JSInvokable]
    public async Task OnInput(ChangeEventArgs args)
    {
        await _owner.HandleOnInputAsync(args);
    }

    [JSInvokable]
    public async Task OnClick(ExMouseEventArgs args)
    {
        await _owner.HandleOnClickAsync(args);
    }

    [JSInvokable]
    public async Task OnMouseUp(ExMouseEventArgs args)
    {
        await _owner.HandleOnMouseUpAsync(args);
    }

    public async Task SetValue(string val)
    {
        ArgumentNullException.ThrowIfNull(_inputJsReference);

        await _inputJsReference.InvokeVoidAsync("setValue", _inputElement, val);
    }

    public async ValueTask DisposeAsync()
    {
        _selfReference?.Dispose();

        if (_inputJsReference != null)
        {
            await _inputJsReference.DisposeAsync();
        }
    }
}
