using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent;

public class BDelayable : BDomComponentBase, IAsyncDisposable
{
    [Parameter]
    public int OpenDelay { get; set; }

    [Parameter]
    public int CloseDelay { get; set; }

    private IJSObjectReference? _module;
    private DotNetObjectReference<BDelayable> _dotNetRef;

    protected bool IsActive { get; private set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetRef = DotNetObjectReference.Create(this);

            _module = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/delayable.js");
            await _module!.InvokeVoidAsync("init", _dotNetRef, OpenDelay, CloseDelay);
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    [JSInvokable]
    public async Task SetActive(bool value)
    {
        await OnActiveUpdating(value);
        IsActive = value;
        await OnActiveUpdated(value);
        StateHasChanged();
    }

    protected virtual Task OnActiveUpdating(bool value)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnActiveUpdated(bool value)
    {
        return Task.CompletedTask;
    }

    protected async Task RunOpenDelayAsync()
    {
        await _module!.InvokeVoidAsync("runDelay", _dotNetRef, "open");
    }

    protected async Task RunCloseDelayAsync()
    {
        await _module!.InvokeVoidAsync("runDelay", _dotNetRef, "close");
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_module is not null && _dotNetRef is not null)
            {
                await _module.InvokeVoidAsync("remove", _dotNetRef);
                await _module.DisposeAsync();
            }

            _dotNetRef?.Dispose();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}