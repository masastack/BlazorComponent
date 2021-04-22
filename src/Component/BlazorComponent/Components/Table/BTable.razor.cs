using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Components.Core.CssProcess;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BTable : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected CssBuilder WrapCssBuilder { get; } = new CssBuilder();

        protected StyleBuilder WrapStyleBuilder { get; } = new StyleBuilder();

        [Parameter]
        public RenderFragment Top { get; set; }

        [Parameter]
        public RenderFragment Bottom { get; set; }
    }
}
