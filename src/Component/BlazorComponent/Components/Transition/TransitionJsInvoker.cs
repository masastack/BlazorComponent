using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent;

public class TransitionJsInvoker : IAsyncDisposable
{
    private readonly IJSRuntime _js;
    private IJSObjectReference? _module;
    private DotNetObjectReference<TransitionJsHelper>? _objRef;

    public TransitionJsInvoker(IJSRuntime js)
    {
        _js = js;
    }

    public async Task Init(Func<string, LeaveOrEnter, Task> onTransitionEnd, Func<Task> onTransitionCancel)
    {
        _module = await _js.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/transition.js");
        _objRef = DotNetObjectReference.Create(new TransitionJsHelper(onTransitionEnd, onTransitionCancel));
    }

    public async Task AddTransitionEvents(object reference, CancellationToken token = default)
    {
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("addTransitionEnd", token, reference, _objRef);
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_module is not null)
            {
                await _module.DisposeAsync();
            }

            _objRef?.Dispose();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}