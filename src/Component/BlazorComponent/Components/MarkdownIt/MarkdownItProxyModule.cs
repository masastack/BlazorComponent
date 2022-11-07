using Microsoft.JSInterop;

namespace BlazorComponent;

public class MarkdownItProxyModule : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public MarkdownItProxyModule(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/markdown-it-proxy.js").AsTask());
    }

    public async Task<MarkdownItProxy> Create(MarkdownItOptions options, bool enableHeaderSections = false, string key = "default")
    {
        key ??= "default";
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("create", options, enableHeaderSections, key);
        return new MarkdownItProxy(module, key);
    }

    public async Task<MarkdownItProxy> Create(MarkdownItOptions options, string key = "default")
    {
        key ??= "default";
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("create", options, false, key);
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
