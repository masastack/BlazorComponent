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

        protected Dictionary<string, object> ContentAttributes
        {
            get
            {
                var attributes = new Dictionary<string, object>(Attributes);

                attributes.Add("onclick", CreateEventCallback<MouseEventArgs>(HandleOnContentClickAsync));

                if (!Disabled && OpenOnHover)
                {
                    attributes.Add("onmouseenter", CreateEventCallback<MouseEventArgs>(HandleOnContentMouseenterAsync));
                }

                if (OpenOnHover)
                {
                    attributes.Add("onmouseleave", CreateEventCallback<MouseEventArgs>(HandleOnContentMouseleaveAsync));
                }

                attributes.Add("close-condition", IsActive);

                return attributes;
            }
        }

        protected async Task HandleOnContentClickAsync(MouseEventArgs _)
        {
            await RunCloseDelayAsync();
        }

        protected async Task HandleOnContentMouseenterAsync(MouseEventArgs args)
        {
            await RunOpenDelayAsync();
        }

        protected async Task HandleOnContentMouseleaveAsync(MouseEventArgs args)
        {
            //TODO:If target is activator
            await RunCloseDelayAsync();
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
