using BlazorComponent.JSInterop;

namespace BlazorComponent;

public class ScrollStrategyJSModule : JSModule
{
    private IJSObjectReference? _instance;

    public ScrollStrategyJSModule(IJSRuntime js) : base(js, "./_content/BlazorComponent/js/scrollStrategies.js")
    {
    }

    public bool Initialized { get; private set; }

    public async Task InitializeAsync(ElementReference root, ElementReference contentRef, object props)
    {
        _instance = await InvokeAsync<IJSObjectReference>("init", root, contentRef, props);

        Initialized = true;
    }

    public async Task HideScroll()
    {
        await _instance.TryInvokeVoidAsync("hideScroll");
    }

    public async Task ShowScroll()
    {
        await _instance.TryInvokeVoidAsync("showScroll");
    }
}
