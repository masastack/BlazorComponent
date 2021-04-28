using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BMessage : BDomComponentBase
    {
        protected CssBuilder WrapperCssBuilder = new();
        protected CssBuilder MessageCssBuilder = new();

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
