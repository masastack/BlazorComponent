using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BHover : BDomComponentBase
    {
        [Parameter]
        public RenderFragment<HoverProps> ChildContent { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        protected bool _isActive { get; set; }

        protected HoverProps Props => new(CssProvider.GetClass(), CssProvider.GetStyle(), $"_b_{Id}", _isActive);
    }
}
