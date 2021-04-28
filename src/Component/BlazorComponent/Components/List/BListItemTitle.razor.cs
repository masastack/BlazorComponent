using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BListItemTitle : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
