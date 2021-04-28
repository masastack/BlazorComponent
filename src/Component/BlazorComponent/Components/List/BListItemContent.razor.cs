using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BListItemContent : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
