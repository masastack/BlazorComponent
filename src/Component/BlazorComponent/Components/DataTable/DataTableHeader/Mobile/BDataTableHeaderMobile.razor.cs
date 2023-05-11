namespace BlazorComponent;

public partial class BDataTableHeaderMobile<TDataTableHeader> where TDataTableHeader : IDataTableHeader
{
    public RenderFragment<DataTableHeader>? HeaderColContent => Component.HeaderColContent;
}
