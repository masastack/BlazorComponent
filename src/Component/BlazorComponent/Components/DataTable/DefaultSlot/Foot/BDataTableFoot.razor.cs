namespace BlazorComponent
{
    public partial class BDataTableFoot<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment? FootContent => Component.FootContent;
    }
}
