namespace BlazorComponent
{
    public partial class BDataTableHeaderDesktop<TDataTableHeader> where TDataTableHeader : IDataTableHeader
    {
        [Parameter]
        public List<DataTableHeader> Headers { get; set; } = null!;

        public bool SingleSelect => Component.SingleSelect;

        public bool DisableSort => Component.DisableSort;

        public bool ShowGroupBy => Component.ShowGroupBy;

        public RenderFragment<DataTableHeader>? HeaderColContent => Component.HeaderColContent;

        public DataOptions Options => Component.Options;

        private async Task HandleOnHeaderColClick(DataTableHeader header)
        {
            if (!DisableSort && header.Sortable)
            {
                await Component.HandleOnHeaderColClick(header.Value);
            }
        }
    }
}
