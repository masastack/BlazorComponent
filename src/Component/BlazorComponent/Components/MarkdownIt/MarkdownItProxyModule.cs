using Microsoft.JSInterop;

namespace BlazorComponent;

public class MarkdownItProxyModule : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public MarkdownItProxyModule(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/markdown-it-proxy.min.js").AsTask());
    }

    public async Task<MarkdownItProxy> Create(MarkdownItOptions options, Dictionary<string, string> tagClassMap,
        bool enableHeaderSections = false,
        string key = "default")
    {
        key ??= "default";
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("create", options, tagClassMap, enableHeaderSections, key);
        return new MarkdownItProxy(module, key);
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
