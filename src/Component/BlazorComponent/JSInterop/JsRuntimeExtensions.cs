namespace BlazorComponent.JSInterop;

public static class JsRuntimeExtensions
{
    public static async Task AddHtmlElementEventListener(this IJSRuntime jsRuntime, string selector, string type, Action callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener,
            selector,
            type,
            DotNetObjectReference.Create(new Invoker(callback)),
            options.Value,
            extras
        );
    }

    public static async Task AddHtmlElementEventListener(this IJSRuntime jsRuntime, string selector, string type, Func<Task> callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener,
            selector,
            type,
            DotNetObjectReference.Create(new Invoker(callback)),
            options.Value,
            extras
        );
    }
    
    public static async Task AddHtmlElementEventListener<T>(this IJSRuntime jsRuntime, string selector, string type, Func<T, Task> callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener,
            selector,
            type,
            DotNetObjectReference.Create(new Invoker<T>(callback)),
            options.Value,
            extras
        );
    }

    public static async Task RemoveHtmlElementEventListener(this IJSRuntime jsRuntime, string selector, string type)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.RemoveHtmlElementEventListener, selector, type);
    }

    public static async Task ScrollTo(this IJSRuntime jsRuntime, string selector, double? top, double? left = null,
        ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollTo, selector, new ScrollToOptions(left, top, behavior));
    }

    public static async Task ScrollTo(this IJSRuntime jsRuntime, ElementReference el, double? top, double? left = null,
        ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        if (el.TryGetSelector(out var selector))
        {
            await jsRuntime.ScrollTo(selector, top, left, behavior);
        }
    }

    public static async Task ScrollIntoView(this IJSRuntime jsRuntime, ElementReference el, ScrollLogicalPosition? block,
        ScrollLogicalPosition? inline, ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollIntoView, el, new ScrollIntoViewOptions(block, inline, behavior));
    }

    public static async Task ScrollToElement(this IJSRuntime jsRuntime, string selector, int offset, ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollToElement, selector, offset, behavior.ToString().ToLower());
    }

    public static async Task ScrollIntoParentView(this IJSRuntime jsRuntime, string selector, bool inline = false, bool start = false,
        int level = 1, ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollIntoParentView, selector, inline, start, level, behavior.ToString().ToLower());
    }

    public static async Task ScrollIntoParentView(this IJSRuntime jsRuntime, ElementReference el, bool inline = false, bool start = false,
        int level = 1, ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        if (el.TryGetSelector(out var selector))
        {
            await jsRuntime.ScrollIntoParentView(selector, inline, start, level, behavior);
        }
    }
}
