namespace BlazorComponent
{
    public partial class BDataTableItems<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public bool IsEmpty => Component.IsEmpty;

        public IEnumerable<IGrouping<string, TItem>> GroupedItems => Component.GroupedItems;

        public IEnumerable<TItem> ComputedItems => Component.ComputedItems;
    }
}
