using BlazorComponent.JSInterop;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent;

public class ActivatableJsInterop : JSModule
{
    private readonly IActivatableJsCallbacks _owner;

    private DotNetObjectReference<ActivatableJsInterop>? _selfReference;
    private IJSObjectReference? _activatableInstance;

    public ActivatableJsInterop(IActivatableJsCallbacks owner, IJSRuntime js) : base(js, "./_content/BlazorComponent/js/activatable.js")
    {
        _owner = owner;
    }

    public async ValueTask InitializeAsync(string? activatorSelector = null)
    {
        _selfReference = DotNetObjectReference.Create(this);
        _activatableInstance = await InvokeAsync<IJSObjectReference>("init",
            activatorSelector ?? _owner.ActivatorSelector,
            _owner.Disabled,
            _owner.OpenOnClick,
            _owner.OpenOnHover,
            _owner.OpenOnFocus,
            _owner.OpenDelay,
            _owner.CloseDelay,
            _selfReference
        );
    }

    public async Task ResetActivator(string selector)
    {
        if (_activatableInstance == null) return;

        await _activatableInstance.InvokeVoidAsync("resetActivator", selector);
    }

    public async Task ResetEvents()
    {
        if (_activatableInstance == null) return;

        await _activatableInstance.InvokeVoidAsync("resetActivatorEvents", _owner.Disabled, _owner.OpenOnHover, _owner.OpenOnFocus);
    }

    public async Task SetActive(bool val)
    {
        if (_activatableInstance == null) return;

        await _activatableInstance.InvokeVoidAsync("setActive", val);
    }

    public async Task RunDelay(bool val)
    {
        if (_activatableInstance == null) return;

        await _activatableInstance.InvokeVoidAsync("runDelaying", val);
    }

    public async Task RegisterPopup(string popupSelector, bool closeOnContentClick)
    {
        if (_activatableInstance == null) return;

        await _activatableInstance.InvokeVoidAsync("registerPopup", popupSelector, closeOnContentClick);
    }

    public async Task ResetPopupEvents(bool closeOnContentClick)
    {
        if (_activatableInstance == null) return;

        await _activatableInstance.InvokeVoidAsync("resetPopupEvents", closeOnContentClick);
    }

    [JSInvokable("SetActive")]
    public async Task JSSetActive(bool val)
    {
        await _owner.SetActive(val);
    }

    [JSInvokable]
    public async Task OnClick(MouseEventArgs args)
    {
        await _owner.HandleOnClickAsync(args);
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();

        _selfReference?.Dispose();

        if (_activatableInstance != null)
        {
            await _activatableInstance.DisposeAsync();
        }
    }
}
