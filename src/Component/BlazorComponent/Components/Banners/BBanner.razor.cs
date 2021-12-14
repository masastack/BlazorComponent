using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BBanner : BDomComponentBase, IHasProviderComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Value { get; set; } = true;
    }
}
