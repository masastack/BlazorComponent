using BlazorComponent.JSInterop;

namespace BlazorComponent.Components.Transition;

public class TransitionJSModule : JSModule
{
    public TransitionJSModule(IJSRuntime js) : base(js, "./_content/BlazorComponent/js/transition.js")
    {
    }

    public bool Initialized { get; private set; }

    public ValueTask<IJSObjectReference?> InitAsync(ElementReference elementReference,
        DotNetObjectReference<TransitionJsInteropHandle> interopHandle)
    {
        Initialized = true;
        return InvokeAsync<IJSObjectReference>("init", elementReference, interopHandle);
    }
}