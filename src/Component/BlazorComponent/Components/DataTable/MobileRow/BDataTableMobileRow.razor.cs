namespace BlazorComponent;

public partial class BDataTableMobileRow<TItem>
{
    [Parameter]
    public List<DataTableHeader<TItem>> Headers { get; set; } = null!;

    [Parameter]
    public bool HideDefaultHeader { get; set; }

    [Parameter]
    public TItem Item { get; set; } = default!;

    [Parameter]
    public int Index { get; set; }

    [Parameter]
    public Func<ItemColProps<TItem>, bool> HasSlot { get; set; } = null!;

    [Parameter]
    public RenderFragment<ItemColProps<TItem>> SlotContent { get; set; } = null!;
}
