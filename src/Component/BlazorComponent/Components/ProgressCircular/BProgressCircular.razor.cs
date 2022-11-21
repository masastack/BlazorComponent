using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BProgressCircular : BDomComponentBase, IProgressCircular
    {
        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; }

        [Parameter]
        public StringNumber Size { get; set; } = 32;

        [Parameter]
        public StringNumber Rotate { get; set; } = 0;

        [Parameter]
        public StringNumber Width { get; set; } = 4;

        [Parameter]
        public StringNumber Value { get; set; } = 0;

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
