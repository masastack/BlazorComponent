using Microsoft.JSInterop;

namespace BlazorComponent;

public class MarkdownItProxy : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public MarkdownItProxy(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/markdown-it-proxy.js").AsTask());
    }

    public async Task Init(MarkdownItOptions options, Dictionary<string, string> tagClassMap, string key = "default")
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("init", options, tagClassMap, key);
    }

    public async Task<string> Render(string mdStr)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string>("render", mdStr);
    }

    public async ValueTask DisposeAsync()
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
