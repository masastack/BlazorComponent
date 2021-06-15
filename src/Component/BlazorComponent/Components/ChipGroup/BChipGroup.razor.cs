using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BChipGroup : BDomComponentBase
    {
        [CascadingParameter()]
        public BChipGroup ChipGroup { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
