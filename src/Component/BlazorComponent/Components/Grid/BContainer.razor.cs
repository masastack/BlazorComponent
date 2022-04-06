using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BContainer : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public virtual string Tag { get; set; } = "div";
    }
}
