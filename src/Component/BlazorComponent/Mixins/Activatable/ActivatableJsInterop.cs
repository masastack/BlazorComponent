using Microsoft.JSInterop;

namespace BlazorComponent;

public interface IActivatableJsCallbacks
{
    string ActivatorSelector { get; }

    bool Disabled { get; }

    bool OpenOnHover { get; }

    bool OpenOnClick { get; }

    bool OpenOnFocus { get; }

    Task SetActive(bool val);
}

public class ActivatableJsInterop : IAsyncDisposable
{
    private readonly IActivatableJsCallbacks _owner;
    private readonly IJSRuntime _jsRuntime;

    private DotNetObjectReference<ActivatableJsInterop>? _selfReference;
    private IJSObjectReference? _jsReference;

    public ActivatableJsInterop(IActivatableJsCallbacks owner, IJSRuntime jsRuntime)
    {
        _owner = owner;
        _jsRuntime = jsRuntime;
    }

    public async ValueTask InitializeAsync()
    {
        _selfReference = DotNetObjectReference.Create(this);
        _jsReference = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/activatable.js");
        await _jsReference!.InvokeVoidAsync("init",
            _owner.ActivatorSelector,
            _owner.Disabled,
            _owner.OpenOnClick,
            _owner.OpenOnHover,
            _owner.OpenOnFocus,
            _selfReference
        );
    }

    [JSInvokable]
    public async Task SetActive(bool val)
    {
        await _owner.SetActive(val);
    }

    public async ValueTask DisposeAsync()
    {
        _selfReference?.Dispose();

        if (_jsReference != null)
        {
            await _jsReference.DisposeAsync();
        }
    }
}
