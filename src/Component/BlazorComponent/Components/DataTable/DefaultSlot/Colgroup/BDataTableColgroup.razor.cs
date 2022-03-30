namespace BlazorComponent
{
    public partial class BDataTableColgroup<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public IEnumerable<DataTableHeader<TItem>> ComputedHeaders => Component.ComputedHeaders;
    }
}
