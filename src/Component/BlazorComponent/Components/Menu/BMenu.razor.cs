using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;

namespace BlazorComponent
{
    public abstract partial class BMenu : BDomComponentBase
    {
        protected bool _visible;

        [Parameter]
        public bool OffsetX { get; set; }

        [Parameter]
        public bool OffsetY { get; set; }

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public StringOrNumber NudgeTop { get; set; }

        [Parameter]
        public StringOrNumber NudgeBottom { get; set; }

        [Parameter]
        public StringOrNumber NudgeLeft { get; set; }

        [Parameter]
        public StringOrNumber NudgeRight { get; set; }

        [Parameter]
        public StringOrNumber NudgeWidth { get; set; }

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool OpenOnHover { get; set; }

        [Parameter]
        public bool CloseOnClick { get; set; } = true;

        [Parameter]
        public bool CloseOnContentClick { get; set; } = true;

        [Parameter]
        public RenderFragment Activator { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public ElementReference ContentRef { get; set; }

        protected abstract void Click(MouseEventArgs args);

        protected virtual void MouseOver(MouseEventArgs args)
        {
            if (OpenOnHover && !_visible)
            {
                _visible = true;
            }
        }

        protected virtual void MouseOut(EventArgs args)
        {
            if (OpenOnHover && _visible)
            {
                _visible = false;
            }
        }
    }
}
