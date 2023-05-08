namespace BlazorComponent
{
    public partial class BDataIteratorEmpty<TItem, TComponent> where TComponent : IDataIterator<TItem>
    {
        public IEnumerable<TItem> Items => Component.Items;

        public DataPagination Pagination => Component.Pagination;

        public StringBoolean? Loading => Component.Loading;

        public RenderFragment? LoadingContent => Component.LoadingContent;

        public string LoadingText => Component.LoadingText;

        public RenderFragment? NoDataContent => Component.NoDataContent;

        public string NoDataText => Component.NoDataText;

        public RenderFragment? NoResultsContent => Component.NoResultsContent;

        public string NoResultsText => Component.NoResultsText;
    }
}
