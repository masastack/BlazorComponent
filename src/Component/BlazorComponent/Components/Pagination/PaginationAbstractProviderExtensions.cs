namespace BlazorComponent
{
    public static class PaginationAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyPaginationDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BPaginationItems<>), typeof(BPaginationItems<IPagination>))
                .Apply(typeof(BPaginationIcon<>), typeof(BPaginationIcon<IPagination>))
                .Apply(typeof(BPaginationList<>), typeof(BPaginationList<IPagination>));
        }
    }
}
