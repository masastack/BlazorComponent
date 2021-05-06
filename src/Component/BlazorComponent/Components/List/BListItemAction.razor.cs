using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BListItemAction : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
