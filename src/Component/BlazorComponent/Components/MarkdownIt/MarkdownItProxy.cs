using Microsoft.JSInterop;

namespace BlazorComponent;

public class MarkdownItProxy
{
    private readonly IJSObjectReference _module;
    private readonly string _key;

    public MarkdownItProxy(IJSObjectReference module, string key)
    {
        _module = module;
        _key = key;
    }

    public async Task<string> Render(string source)
    {
        return await _module.InvokeAsync<string>("render", source, _key);
    }
}
