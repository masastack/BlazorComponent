using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTimelineItem : BDomComponentBase
    {
        [CascadingParameter]
        protected BTimeline BTimeline { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment OppositeContent { get; set; }
    }
}
