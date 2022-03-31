using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BToolbarItems : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
