namespace BlazorComponent;

public class TransitionJsInvoker : IAsyncDisposable
{
    private readonly IJSRuntime _js;

    private bool _isDisposed;
    private IJSObjectReference? _module;
    private DotNetObjectReference<TransitionJsHelper>? _objRef;

    public TransitionJsInvoker(IJSRuntime js) => _js = js;

    public async Task Init(Func<string, LeaveEnter, Task> onTransitionEnd, Func<string, LeaveEnter, Task> onTransitionCancel)
    {
        _module = await _js.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/transition.js");
        _objRef = DotNetObjectReference.Create(new TransitionJsHelper(onTransitionEnd, onTransitionCancel));
    }

    public async Task RegisterTransitionEvents(object reference, CancellationToken token = default)
    {
        if (_module is not null && !_isDisposed)
        {
            await _module.InvokeVoidAsync("registerTransitionEvents", token, reference, _objRef);
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            _objRef?.Dispose();

            if (_module is not null)
            {
                _isDisposed = true;
                await _module.DisposeAsync();
            }
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
    }
}
