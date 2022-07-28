namespace BlazorComponent;

public partial class BDataTableHeaderSortSelect<TDataTableHeader> where TDataTableHeader : IDataTableHeader
{
    [Parameter] public List<(string Text, string Value)> Items { get; set; }
}
