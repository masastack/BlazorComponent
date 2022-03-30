using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BHover : BDomComponentBase
    {
        [Parameter]
        public RenderFragment<HoverProps> ChildContent { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        protected virtual bool IsActive { get; }

        protected HoverProps Props => new(CssProvider.GetClass(), CssProvider.GetStyle(), $"_b_{Id}", IsActive);
    }
}
