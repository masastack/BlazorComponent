namespace BlazorComponent;

public class InputJsInterop : IAsyncDisposable
{
    private readonly IInputJsCallbacks _owner;
    private readonly IJSRuntime _jsRuntime;

    private DotNetObjectReference<InputJsInterop>? _selfReference;
    private IJSObjectReference? _inputJsReference;
    private bool _isDisposed;

    private ElementReference? _inputElement;

    public bool Initialized { get; private set; }

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

        if (_isDisposed) return;

        await _inputJsReference!.InvokeVoidAsync("registerInputEvents", input, inputSlot, _selfReference, internalDebounceInterval);

        Initialized = true;
    }

    [JSInvokable]
    public async Task OnInput(ChangeEventArgs args)
    {
        await _owner.HandleOnInputAsync(args);
        _owner.StateHasChangedForJsInvokable();
    }

    [JSInvokable]
    public async Task OnClick(ExMouseEventArgs args)
    {
        await _owner.HandleOnClickAsync(args);
        _owner.StateHasChangedForJsInvokable();
    }

    [JSInvokable]
    public async Task OnMouseUp(ExMouseEventArgs args)
    {
        await _owner.HandleOnMouseUpAsync(args);
        _owner.StateHasChangedForJsInvokable();
    }

    public async Task SetValue(string? val)
    {
        ArgumentNullException.ThrowIfNull(_inputJsReference);

        await _inputJsReference.InvokeVoidAsync("setValue", _inputElement, val);
    }

    public async ValueTask DisposeAsync()
    {
        _isDisposed = true;

        _selfReference?.Dispose();

        if (_inputJsReference != null)
        {
            await _inputJsReference.DisposeAsync();
        }
    }
}
