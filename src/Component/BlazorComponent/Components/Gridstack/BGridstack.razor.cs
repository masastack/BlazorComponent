using Microsoft.JSInterop;

namespace BlazorComponent;

public partial class BGridstack<TItem> : BDomComponentBase, IAsyncDisposable
{
    [Inject]
    protected GridstackProxyModule Module { get; set; } = null!;

    [Parameter, EditorRequired]
    public List<TItem> Items { get; set; } = new();

    [Parameter, EditorRequired]
    public RenderFragment<TItem> ItemContent { get; set; } = null!;

    [Parameter, EditorRequired]
    public Func<TItem, string> ItemKey { get; set; } = null!;

    [Parameter]
    public Func<TItem, int>? ItemColumn { get; set; }

    [Parameter]
    public bool Readonly
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    [Parameter]
    public int Column { get; set; } = 12;

    [Parameter]
    public int MinRow { get; set; }

    [Parameter]
    public EventCallback<(ElementReference? elementReference, string? id, int width, int height)> OnResize { get; set; }

    private string? _prevItemKeys;
    private IJSObjectReference? _gridstackInstance;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        ArgumentNullException.ThrowIfNull(Items);
        ArgumentNullException.ThrowIfNull(ItemContent);
        ArgumentNullException.ThrowIfNull(ItemKey);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Watcher.Watch<bool>(nameof(Readonly), (val) => { _ = SetStatic(val); });
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (Column < 0) Column = 12;

        var itemKeys = string.Join("", Items.Select(ItemKey));
        if (_prevItemKeys is not null && _prevItemKeys != itemKeys)
        {
            _prevItemKeys = itemKeys;
            NextTick(async () => { await Reload(); });
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _prevItemKeys = string.Join("", Items.Select(ItemKey));
            _gridstackInstance = await Module.Init(new { Column, MinRow }, Ref);
            Module.Resize += GridstackOnResize;

            if (Readonly)
            {
                await SetStatic(true);
            }
        }
    }

    private void GridstackOnResize(object sender, GridstackProxyModule.GridstackResizeEventArgs e)
    {
        if (OnResize.HasDelegate)
        {
            OnResize.InvokeAsync((e.ElementReference, e.Id, e.Width, e.Height));
            InvokeStateHasChanged();
        }
    }

    private async Task Reload()
    {
        if (_gridstackInstance is null) return;
        _gridstackInstance = await Module.Reload(_gridstackInstance);
    }

    private async Task SetStatic(bool staticValue)
    {
        if (_gridstackInstance is null) return;
        await Module.SetStatic(_gridstackInstance, staticValue);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            Module.Resize -= GridstackOnResize;
            if (_gridstackInstance is not null)
            {
                await _gridstackInstance.DisposeAsync();
            }
        }
        catch
        {
            // ignored
        }
    }
}
