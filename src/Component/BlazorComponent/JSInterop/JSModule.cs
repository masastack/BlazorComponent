namespace BlazorComponent.JSInterop;

/// <summary>
/// Helper for loading any JavaScript (ES6) module and calling its exports
/// </summary>
public abstract class JSModule : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    protected JSModule(IJSRuntime js, string moduleUrl)
        => _moduleTask = new Lazy<Task<IJSObjectReference>>(() => js.InvokeAsync<IJSObjectReference>("import", moduleUrl).AsTask());

    protected async ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync(identifier, args);
    }

    protected async ValueTask<T> InvokeAsync<T>(string identifier, params object?[]? args)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<T>(identifier, args);
    }

    public async virtual ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            try
            {
                await module.DisposeAsync();
            }
            catch
            {
                // ignored
            }
        }
    }
}
