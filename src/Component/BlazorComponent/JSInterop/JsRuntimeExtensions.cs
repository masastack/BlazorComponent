using Microsoft.JSInterop;

namespace BlazorComponent.JSInterop;

public static class JsRuntimeExtensions
{
    public static async Task AddOutsideClickEventListener(this IJSRuntime jsRuntime, Func<ClickOutsideArgs, Task> callback,
        IEnumerable<string> noInvokeSelectors, IEnumerable<string> invokeSelectors = null)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.AddOutsideClickEventListener,
            DotNetObjectReference.Create(new Invoker<ClickOutsideArgs>(callback)),
            noInvokeSelectors, invokeSelectors);
    }
}
