namespace BlazorComponent;

public partial class BDataTableColgroup<TItem>
{
    [Parameter]
    public List<DataTableHeader<TItem>> Headers { get; set; } = new();

    private bool HasFixedColumn => Headers.Any(u => u.Fixed != DataTableFixed.None);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var lastFixedLeftHeader = Headers.LastOrDefault(u => u.Fixed == DataTableFixed.Left);
        if (lastFixedLeftHeader != null)
        {
            lastFixedLeftHeader.IsFixedShadowColumn = true;
        }

        var firstFixedRightHeader = Headers.FirstOrDefault(u => u.Fixed == DataTableFixed.Right);
        if (firstFixedRightHeader != null)
        {
            firstFixedRightHeader.IsFixedShadowColumn = true;
        }
    }
}
