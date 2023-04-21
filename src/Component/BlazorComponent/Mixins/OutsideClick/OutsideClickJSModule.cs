using BlazorComponent.JSInterop;

namespace BlazorComponent;

public class OutsideClickJSModule : JSModule
{
    private IOutsideClickJsCallback? _owner;
    private DotNetObjectReference<OutsideClickJSModule>? _selfReference;
    private IJSObjectReference? _instance;

    public OutsideClickJSModule(IJSRuntime js) : base(js, "./_content/BlazorComponent/js/outside-click.js")
    {
    }

    public bool Initialized { get; private set; }
    
    public bool IsUpdating { get; private set; }

    public async ValueTask InitializeAsync(IOutsideClickJsCallback owner, params string[] excludedSelectors)
    {
        _owner = owner;
        _selfReference = DotNetObjectReference.Create(this);
        _instance = await InvokeAsync<IJSObjectReference>("init", _selfReference, excludedSelectors);

        Initialized = true;
    }
    
    private CancellationTokenSource _cts = new();

    public async Task UpdateDependentElements(params string[] selectors)
    {
        Console.Out.WriteLine("_instance is null = {0}", _instance is null);
        if (_instance is null) return;

        Console.Out.WriteLine($"UpdateDependentElements {selectors.Length}");
        
        _cts.Cancel();
        _cts = new CancellationTokenSource();
        await Task.Delay(100, _cts.Token);

        Console.Out.WriteLine("UpdateDependentElements js.................");
        
        await _instance.InvokeAsync<bool>("updateExcludeSelectors", selectors.ToList());
    }

    [JSInvokable]
    public async Task OnOutsideClick()
    {
        if (_owner == null) return;

        await _owner.HandleOnOutsideClickAsync();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();

        _selfReference?.Dispose();

        if (_instance is not null)
        {
            await _instance.DisposeAsync();
        }
    }
}
