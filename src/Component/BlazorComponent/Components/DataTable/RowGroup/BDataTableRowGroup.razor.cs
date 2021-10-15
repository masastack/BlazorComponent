using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableRowGroup
    {
        [Parameter]
        public RenderFragment RowHeaderContent { get; set; }

        [Parameter]
        public RenderFragment RowContentContent { get; set; }

        [Parameter]
        public RenderFragment ColumnHeaderContent { get; set; }

        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public virtual string HeaderClass { get; }
    }
}
