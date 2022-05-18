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

    public Func<Task>? AfterShowContent { get; set; }

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

    [JSInvokable("SetActive")]
    public async Task SetIsActive(bool value)
    {
        if (value)
        {
            await ShowLazyContent();
        }

        await WhenIsActiveUpdating(value);

        IsActive = value;

        StateHasChanged();

        if (AfterShowContent is not null)
        {
            await AfterShowContent();
        }
    }

    protected virtual Task ShowLazyContent()
    {
        return Task.CompletedTask;
    }

    protected virtual Task WhenIsActiveUpdating(bool value)
    {
        return Task.CompletedTask;
    }

    protected async Task RunOpenDelayAsync()
    {
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("runDelay", _dotNetRef, "open");
        }
        else
        {
            await SetIsActive(true);
        }
    }

    protected async Task RunCloseDelayAsync()
    {
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("runDelay", _dotNetRef, "close");
        }
        else
        {
            await SetIsActive(false);
        }
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