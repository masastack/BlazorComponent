using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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
        public StringNumber ClientX { get; set; }

        [Parameter]
        public StringNumber ClientY { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public string Absolute { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public bool PreventDefault { get; set; }

        protected virtual async Task HandleOnClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }
    }
}
