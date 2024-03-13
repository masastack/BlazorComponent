using BlazorComponent.JSInterop;

namespace BlazorComponent.Components.Transition;

public class TransitionJSModule : JSModule
{
    private IJSObjectReference? _instance;
    private DotNetObjectReference<TransitionJSModule> _selfReference;
    private ITransitionJSCallback _owner;

    public TransitionJSModule(IJSRuntime js) : base(js, "./_content/BlazorComponent/js/transition.js")
    {
    }

    public bool Initialized { get; private set; }

    public async Task InitializeAsync(ITransitionJSCallback owner)
    {
        _owner = owner;
        _selfReference = DotNetObjectReference.Create(this);

        _instance = await InvokeAsync<IJSObjectReference>("init",
            _selfReference,
            owner.TransitionName,
            owner.Reference,
            owner.LeaveAbsolute);

        Initialized = true;
    }

    [JSInvokable]
    public async Task OnTransitionEnd()
    {
        await _owner.HandleOnTransitionend();
    }

    public async Task OnEnter()
    {
        Console.Out.WriteLine("On enter");
        await _instance.TryInvokeVoidAsync("onEnter");
    }

    public async Task OnEnter(ElementReference elementReference)
    {
        Console.Out.WriteLine("On enter");
        await _instance.TryInvokeVoidAsync("onEnter", elementReference);
    }
    
    public async Task OnEnter(string selector)
    {
        Console.Out.WriteLine("On enter");
        await _instance.TryInvokeVoidAsync("onEnter", selector);
    }
    
    public async Task OnEnterTo(ElementReference elementReference)
    {
        Console.Out.WriteLine("On enter to");
        await _instance.TryInvokeVoidAsync("onEnterTo", elementReference);
    }

    public async Task OnLeave()
    {
        Console.Out.WriteLine("On leave");
        await _instance.TryInvokeVoidAsync("onLeave");
    }
}
