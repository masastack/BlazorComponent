using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using OneOf;

namespace BlazorComponent
{
    public abstract partial class BApp : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected CssBuilder WrapCssBuilder { get; } = new CssBuilder();
    }
}
