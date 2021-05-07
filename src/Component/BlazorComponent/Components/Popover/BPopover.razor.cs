using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BPopover : BDomComponentBase
    {
        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public bool OffsetX { get; set; }

        [Parameter]
        public bool OffsetY { get; set; }

        [Parameter]
        public StringOrNumber ClientX { get; set; }

        [Parameter]
        public StringOrNumber ClientY { get; set; }

        [Parameter]
        public StringOrNumber MinWidth { get; set; }

        [Parameter]
        public StringOrNumber MaxHeight { get; set; }

        [Parameter]
        public string Absolute { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
