using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BBadge : BDomComponentBase, IHasProviderComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool Inline { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public bool IsDark => Dark ?
            true :
            (Light ? false : CascadingIsDark);
    }
}
