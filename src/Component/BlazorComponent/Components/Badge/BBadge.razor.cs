using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BBadge : BDomComponentBase, IHasProviderComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool InLine { get; set; }

        [Parameter]
        public bool Left { get; set; }
    }
}
