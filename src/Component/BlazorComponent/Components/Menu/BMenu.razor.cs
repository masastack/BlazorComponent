using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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
        public bool Absolute { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool OpenOnHover { get; set; }

        [Parameter]
        public RenderFragment Slot { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected abstract void Click(MouseEventArgs args);

        protected virtual void MouseOver(MouseEventArgs args)
        {
            if (OpenOnHover && !_visible)
            {
                _visible = true;

                Console.WriteLine("over");
            }
        }

        protected virtual void MouseOut(EventArgs args)
        {
            if (OpenOnHover && _visible)
            {
                _visible = false;
                Console.WriteLine("out");
            }
        }
    }
}
