namespace BlazorComponent
{
    public partial class BDataTableDefaultExpandedRow<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public TItem Item { get; set; }

        public bool IsExpanded => Component.IsExpanded(Item);

        public RenderFragment<(IEnumerable<DataTableHeader<TItem>> Headers, TItem Item)> ExpandedItemContent => Component.ExpandedItemContent;

        public (IEnumerable<DataTableHeader<TItem>> Headers, TItem Item) Props => (Component.ComputedHeaders, Item);
    }
}
