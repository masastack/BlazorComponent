namespace BlazorComponent;

public partial class BDataTableHeaderSortSelect<TDataTableHeader> where TDataTableHeader : IDataTableHeader
{
    [Parameter]
    public List<DataTableHeader>? Items { get; set; }

    [Parameter]
    public RenderFragment<SelectSelectionProps<DataTableHeader>>? SelectionContent { get; set; }

    [Parameter]
    public RenderFragment<SelectListItemProps<DataTableHeader>>? ItemContent { get; set; }

    [Parameter]
    public bool MultiSort { get; set; }
}
