using System.Collections.Generic;
using BlazorComponent.Components.Core.CssProcess;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BCard : BDomComponentBase
    {
        protected CssBuilder CardCoverCssBuilder { get; } = new CssBuilder();
        protected StyleBuilder CoverStyleBuilder { get; } = new StyleBuilder();

        protected CssBuilder TitleCssBuilder { get; } = new CssBuilder();
        protected CssBuilder SubTitleCssBuilder { get; } = new CssBuilder();

        protected CssBuilder TextCssBuilder { get; } = new CssBuilder();
        protected CssBuilder ActionsCssBuilder { get; } = new CssBuilder();

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public RenderFragment Cover { get; set; }

        [Parameter]
        public RenderFragment Title { get; set; }

        [Parameter]
        public RenderFragment SubTitle { get; set; }

        [Parameter]
        public RenderFragment Text { get; set; }

        [Parameter]
        public RenderFragment Actions { get; set; }

    }
}
