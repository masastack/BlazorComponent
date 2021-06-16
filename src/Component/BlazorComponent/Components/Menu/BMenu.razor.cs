using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract partial class BMenu : BDomComponentBase
    {
        //protected bool _visible;

        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public EventCallback<bool> VisibleChanged { get; set; }

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
        public StringNumber NudgeTop { get; set; }

        [Parameter]
        public StringNumber NudgeBottom { get; set; }

        [Parameter]
        public StringNumber NudgeLeft { get; set; }

        [Parameter]
        public StringNumber NudgeRight { get; set; }

        [Parameter]
        public StringNumber NudgeWidth { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

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
        public string ActivatorStyle { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public ElementReference ContentRef { get; set; }

        protected abstract Task Click(MouseEventArgs args);

        protected virtual Task MouseEnter(MouseEventArgs args)
        {
            if (OpenOnHover && !Visible && VisibleChanged.HasDelegate)
                VisibleChanged.InvokeAsync(true);

            return Task.CompletedTask;
        }

        protected virtual Task MouseOut(MouseEventArgs args)
        {
            if (OpenOnHover && Visible && VisibleChanged.HasDelegate)
                VisibleChanged.InvokeAsync(false);

            return Task.CompletedTask;
        }
    }
}
