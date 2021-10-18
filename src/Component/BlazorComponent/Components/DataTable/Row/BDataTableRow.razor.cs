using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableRow<TItem>
    {
        [Parameter]
        public List<DataTableHeader<TItem>> Headers { get; set; }

        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public RenderFragment<ItemColProps<TItem>> ItemColContent { get; set; }

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
