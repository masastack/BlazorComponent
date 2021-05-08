using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BOverlay : BDomComponentBase
    {
        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public string Color { get; set; } = "#212121";

        [Parameter]
        public StringOrNumber Opacity { get; set; } = 0.46;

        /// <summary>
        /// Controls whether the component is visible or hidden.
        /// </summary>
        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public StringOrNumber ZIndex { get; set; } = 5;

        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private void HandleOnClick(MouseEventArgs args)
        {
            if (Click.HasDelegate)
            {
                Click.InvokeAsync(args);
            }
            else
            {
                ValueChanged.InvokeAsync(false);
            }
        }
    }
}
