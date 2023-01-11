using BlazorComponent.JSInterop;
using Microsoft.JSInterop;

namespace BlazorComponent;

public class OutsideClickJSModule : JSModule
{
    private readonly IOutsideClickJsCallback _owner;
    private DotNetObjectReference<OutsideClickJSModule>? _selfReference;
    private IJSObjectReference? _module;

    public OutsideClickJSModule(IOutsideClickJsCallback owner, IJSRuntime js) : base(js, "./_content/BlazorComponent/js/outside-click.js")
    {
        _owner = owner;
    }

    public async ValueTask InitializeAsync(params string[] excludedSelectors)
    {
        _selfReference = DotNetObjectReference.Create(this);
        _module = await InvokeAsync<IJSObjectReference>("init", _selfReference, excludedSelectors);
    }

    [JSInvokable]
    public async Task OnOutsideClick()
    {
        await _owner.HandleOnOutsideClickAsync();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();

        _selfReference?.Dispose();

        if (_module is not null)
        {
            await _module.DisposeAsync();
        }
    }
}
