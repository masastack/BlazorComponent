using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BExpansionPanelContent : BDomComponentBase
    {
        [CascadingParameter]
        public BExpansionPanels ExpansionPanels { get; set; }

        [CascadingParameter]
        public BExpansionPanel ExpansionPanel { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
