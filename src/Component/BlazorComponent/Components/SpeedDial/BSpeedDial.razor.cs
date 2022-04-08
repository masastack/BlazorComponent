using BlazorComponent.Mixins;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public partial class BSpeedDial : BBootable
    {
        [Inject]
        public Document Document { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Direction { get; set; } = "top";

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public string Transition { get; set; } = "scale-transition";

        [Parameter]
        public string Origin { get; set; }

        protected ElementReference ContentElement { get; set; }

        private string Tag { get; set; } = "div";

        protected override async Task OnActiveUpdated(bool value)
        {
            if (!OpenOnHover)
            {
                await JsInvokeAsync(JsInteropConstants.AddOutsideClickEventListener,
                    DotNetObjectReference.Create(new Invoker<object>(HandleOutsideClickAsync)),
                    new[] { Document.GetElementByReference(ContentElement).Selector, ActivatorSelector }, null, ContentElement);
            }

            await base.OnActiveUpdated(value);
        }

        private async Task HandleOutsideClickAsync(object agrs)
        {
            if (!IsActive) return;

            await RunCloseDelayAsync();
        }

        protected override async Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (Value)
            {
                await RunCloseDelayAsync();
            }
            else
            {
                await RunOpenDelayAsync();
            }
        }
    }
}
