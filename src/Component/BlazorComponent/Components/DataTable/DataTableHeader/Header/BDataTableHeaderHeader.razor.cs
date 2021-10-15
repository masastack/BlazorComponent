using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
