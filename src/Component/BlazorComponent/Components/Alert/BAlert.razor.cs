using BlazorComponent.Components.Core.CssProcess;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BAlert : BDomComponentBase
    {
        protected CssBuilder WrapperCssBuilder = new ();
        protected CssBuilder ContentCssBuilder = new ();
        protected CssBuilder BorderCssBuilder = new ();
        protected StyleBuilder BorderStyleBuilder = new ();

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Prominent { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Text { get; set; }

        [Parameter]
        public bool Dismissible { get; set; }

        protected RenderFragment DismissibleButtonContent { get; set; }

        [Parameter]
        public AlertBorder? Border { get; set; }

        [Parameter]
        public bool ColoredBorder { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public string Icon { get; set; }

        protected RenderFragment IconContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Visible { get; set; } = true;

        [Parameter]
        public EventCallback<bool> VisibleChanged { get; set; }
    }
}
