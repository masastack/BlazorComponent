using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BHitMessage : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}