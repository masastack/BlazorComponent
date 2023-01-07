using BlazorComponent.JSInterop;
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

        private string Tag { get; set; } = "div";

        private bool _isAttached;

        protected ElementReference ContentElement { get; set; }

        protected override async Task WhenIsActiveUpdating(bool value)
        {
            if (!OpenOnHover && !_isAttached)
            {
                _isAttached = true;

                await Js.AddOutsideClickEventListener(HandleOutsideClickAsync, new[] { ContentElement.GetSelector(), ActivatorSelector });
            }

            await base.WhenIsActiveUpdating(value);
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
            RunDirectly(false);
        }

        protected async Task HandleOnContentMouseenterAsync(MouseEventArgs args)
        {
            RunDelaying(true);
        }

        protected async Task HandleOnContentMouseleaveAsync(MouseEventArgs args)
        {
            RunDelaying(false);
        }

        private async Task HandleOutsideClickAsync(object agrs)
        {
            if (!IsActive) return;

            RunDirectly(false);
        }
    }
}
