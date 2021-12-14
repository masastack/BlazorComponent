using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSimpleTable
    {
        [Parameter]
        public RenderFragment TopContent { get; set; }

        [Parameter]
        public RenderFragment BottomContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
