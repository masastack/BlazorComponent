namespace BlazorComponent
{
    public partial class BDataFooterItemsPerPageSelect<TComponent> where TComponent : IDataFooter
    {
        public IEnumerable<DataItemsPerPageOption> ComputedDataItemsPerPageOptions => Component.ComputedDataItemsPerPageOptions;

        public string? ItemsPerPageText => Component.ItemsPerPageText;
    }
}
