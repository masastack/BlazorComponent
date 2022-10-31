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

    public async Task<string> Parse(string source)
    {
        return await _module.InvokeAsync<string>("parse", source, _key);
    }

    public async Task<string> Highlight(string code, string lang)
    {
        return await _module.InvokeAsync<string>("highlight", code, lang);
    }
}
