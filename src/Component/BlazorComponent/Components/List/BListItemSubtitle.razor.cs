using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BListItemSubtitle : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
