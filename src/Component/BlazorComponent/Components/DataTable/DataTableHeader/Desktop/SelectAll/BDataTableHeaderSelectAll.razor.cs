namespace BlazorComponent
{
    public partial class BDataTableHeaderSelectAll<TDataTableHeader> where TDataTableHeader : IDataTableHeader
    {
        public RenderFragment? DataTableSelectContent => Component.DataTableSelectContent;
    }
}
