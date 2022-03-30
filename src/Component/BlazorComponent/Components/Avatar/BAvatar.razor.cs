using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BAvatar : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
