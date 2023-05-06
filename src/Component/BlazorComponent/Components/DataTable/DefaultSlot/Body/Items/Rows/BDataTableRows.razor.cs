namespace BlazorComponent
{
    public partial class BDataTableRows<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        [Parameter]
        public IEnumerable<TItem> Items { get; set; }

        public RenderFragment<ItemProps<TItem>> ItemContent => Component.ItemContent;
    }
}
