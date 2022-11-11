using Microsoft.JSInterop;

namespace BlazorComponent;

public partial class BGridstack<TItem> : BDomComponentBase, IAsyncDisposable
{
    [Parameter, EditorRequired]
    public List<TItem> Items { get; set; } = new();

    [Parameter, EditorRequired]
    public RenderFragment<TItem> ItemContent { get; set; } = null!;

    [Parameter, EditorRequired]
    public Func<TItem, string> ItemKey { get; set; } = null!;

    [Parameter]
    public Func<TItem, int>? ItemColumn { get; set; }

    [Parameter]
    public int Column { get; set; } = 12;

    [Parameter]
    public int MinRow { get; set; }

    private string? _prevItemKeys;
    private IJSObjectReference? _jsObjectReference;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        ArgumentNullException.ThrowIfNull(Items);
        ArgumentNullException.ThrowIfNull(ItemContent);
        ArgumentNullException.ThrowIfNull(ItemKey);
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

            _jsObjectReference = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/gridstack-proxy.js");
            await _jsObjectReference!.InvokeVoidAsync("init", Ref, new
            {
                Column, // TODO: not work?
                MinRow
            });
        }
    }

    private async Task Reload()
    {
        if (_jsObjectReference is null) return;
        await _jsObjectReference.InvokeVoidAsync("reload", Ref);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_jsObjectReference is not null)
            {
                await _jsObjectReference.DisposeAsync();
            }
        }
        catch
        {
            // ignored
        }
    }
}
