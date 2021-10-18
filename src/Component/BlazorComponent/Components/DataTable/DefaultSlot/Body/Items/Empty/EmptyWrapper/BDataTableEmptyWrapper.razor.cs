using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableEmptyWrapper<TItem,TDataTable> where TDataTable : IDataTable<TItem>
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public Dictionary<string, object> ColspanAttrs => Component.ColspanAttrs;
    }
}
