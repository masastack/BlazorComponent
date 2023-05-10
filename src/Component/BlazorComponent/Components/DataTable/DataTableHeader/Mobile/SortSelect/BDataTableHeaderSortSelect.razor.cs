namespace BlazorComponent;

public partial class BDataTableHeaderSortSelect<TDataTableHeader> where TDataTableHeader : IDataTableHeader
{
    [Parameter]
    public List<(string Text, string Value)>? Items { get; set; }

    [Parameter]
    public RenderFragment<SelectSelectionProps<(string Text, string Value)>>? SelectionContent { get; set; }

    [Parameter]
    public bool MultiSort { get; set; }
}
