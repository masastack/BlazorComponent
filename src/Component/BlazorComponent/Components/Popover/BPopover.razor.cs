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
        public string Absolute { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected string PopoverStyle =>
            StyleBuilder
                .Clear()
                .Add(() => "z-index: 1000;")
                .Add(() => Visible ? "position: absolute" : "display: none")
                .Add(() => $"top: {ClientY?.TryGetNumber().number ?? 0}px")
                .Add(() => $"left: {ClientX?.TryGetNumber().number ?? 0}px")
                .Add(() => $"min-width: {MinWidth?.TryGetNumber().number ?? 0}px")
                .ToString();
    }
}
