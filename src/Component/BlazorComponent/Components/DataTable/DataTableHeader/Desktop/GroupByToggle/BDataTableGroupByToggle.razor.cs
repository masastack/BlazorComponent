namespace BlazorComponent
{
    public partial class BDataTableGroupByToggle<TDataTableHeader> where TDataTableHeader : IDataTableHeader
    {
        [Parameter]
        public DataTableHeader Header { get; set; } = null!;

        [Parameter]
        public string GroupText { get; set; } = null!;

        public EventCallback<MouseEventArgs> HandleOnGroup => CreateEventCallback<MouseEventArgs>(async _ => await Component.HandleOnGroup(Header.Value));
    }
}
