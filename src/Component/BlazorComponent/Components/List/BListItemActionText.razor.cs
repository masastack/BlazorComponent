using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BListItemActionText : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
