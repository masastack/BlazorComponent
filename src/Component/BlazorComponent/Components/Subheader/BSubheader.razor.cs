using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSubheader : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
