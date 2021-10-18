namespace BlazorComponent
{
    public static class DataFooterAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyDataFooterDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BDataFooterItemsPerPageSelect<>), typeof(BDataFooterItemsPerPageSelect<IDataFooter>))
                .Apply(typeof(BDataFooterPaginationInfo<>), typeof(BDataFooterPaginationInfo<IDataFooter>))
                .Apply(typeof(BDataFooterIcons<>), typeof(BDataFooterIcons<IDataFooter>))
                .Apply(typeof(BDataFooterIcon<>), typeof(BDataFooterIcon<IDataFooter>));
        }
    }
}
