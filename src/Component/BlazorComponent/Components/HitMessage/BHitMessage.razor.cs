using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BHitMessage : BDomComponentBase
    {
        protected CssBuilder WrapperCssBuilder { get; } = new();
        protected CssBuilder MessageCssBuilder { get; } = new();

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}