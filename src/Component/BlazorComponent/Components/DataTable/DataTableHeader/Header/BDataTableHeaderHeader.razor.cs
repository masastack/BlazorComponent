using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataTableHeaderHeader<TDataTableHeader> where TDataTableHeader : IDataTableHeader
    {
        [Parameter]
        public DataTableHeader Header { get; set; }

        public bool SingleSelect => Component.SingleSelect;

        public bool DisableSort => Component.DisableSort;

        public bool ShowGroupBy => Component.ShowGroupBy;

        public RenderFragment<DataTableHeader> HeaderColContent => Component.HeaderColContent;

        public Func<DataTableHeader, Dictionary<string, object>> GetHeaderAttrs => Component.GetHeaderAttrs;

        public DataOptions Options => Component.Options;
    }
}
