using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BSubheader : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
