namespace BlazorComponent
{
    public partial class BDataTableDefaultRows<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment<(IEnumerable<DataTableHeader<TItem>> Headers, TItem Item)> ExpandedItemContent => Component.ExpandedItemContent;

        [Parameter]
        public IEnumerable<TItem> Items { get; set; }
    }
}
