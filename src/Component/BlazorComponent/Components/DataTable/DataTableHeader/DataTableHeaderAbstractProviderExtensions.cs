namespace BlazorComponent
{
    public static class DataTableHeaderAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyDataTableHeaderDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BDataTableHeaderHeader<>), typeof(BDataTableHeaderHeader<IDataTableHeader>))
                .Apply(typeof(BDataTableHeaderSelectAll<>), typeof(BDataTableHeaderSelectAll<IDataTableHeader>))
                .Apply(typeof(BDataTableHeaderSortIcon<>), typeof(BDataTableHeaderSortIcon<IDataTableHeader>))
                .Apply(typeof(BDataTableGroupByToggle<>), typeof(BDataTableGroupByToggle<IDataTableHeader>));
        }
    }
}
