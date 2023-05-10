using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BListItemIcon : BDomComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
