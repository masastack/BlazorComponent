namespace BlazorComponent
{
    public partial class BDataTableLoading<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public Dictionary<string, object> ColspanAttrs => Component.ColspanAttrs;
    }
}
