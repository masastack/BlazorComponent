namespace BlazorComponent;

public partial class BSelectListTile<TItem, TItemValue, TValue>
{
    [Parameter]
    public TItem? Item { get; set; }

    [Parameter]
    public int Index { get; set; }

    [Parameter]
    public bool? Disabled { get; set; }

    protected bool Action => Component.Action;

    protected bool HideSelected => Component.HideSelected;

    protected IList<TItem> Items => Component.Items;

    protected RenderFragment<SelectListItemProps<TItem>> ItemContent => Component.ItemContent;

    private bool Value => Item != null && Component.HasItem(Item);

    private bool IsDisabled => Item != null && Component.GetDisabled(Item);

    private async Task HandleOnClick()
    {
        if (IsDisabled)
        {
            return;
        }

        await Component.OnSelect.InvokeAsync(Item);
    }
}
