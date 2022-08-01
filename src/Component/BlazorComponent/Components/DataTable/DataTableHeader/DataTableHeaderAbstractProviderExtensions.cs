namespace BlazorComponent
{
    public static class DataTableHeaderAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyDataTableHeaderDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                   .Apply(typeof(BDataTableHeaderDesktop<>), typeof(BDataTableHeaderDesktop<IDataTableHeader>))
                   .Apply(typeof(BDataTableHeaderMobile<>), typeof(BDataTableHeaderMobile<IDataTableHeader>))
                   .Apply(typeof(BDataTableHeaderSelectAll<>), typeof(BDataTableHeaderSelectAll<IDataTableHeader>))
                   .Apply(typeof(BDataTableHeaderSortIcon<>), typeof(BDataTableHeaderSortIcon<IDataTableHeader>))
                   .Apply(typeof(BDataTableGroupByToggle<>), typeof(BDataTableGroupByToggle<IDataTableHeader>))
                   .Apply(typeof(BDataTableHeaderSortSelect<>), typeof(BDataTableHeaderSortSelect<IDataTableHeader>));
        }
    }
}
