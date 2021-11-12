using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BLabel : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string For { get; set; }

        [Parameter]
        public bool Required { get; set; }

        [Parameter]
        public string Tag { get; set; } = "label";
    }
}