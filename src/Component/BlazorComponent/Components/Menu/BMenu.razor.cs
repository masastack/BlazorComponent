using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract partial class BMenu : BDomComponentBase
    {
        [CascadingParameter(Name = "Fixed")]
        public bool Fixed { get; set; }

        protected bool _visible;
        [Parameter]
        public bool Visible
        {
            get => _visible;
            set => _visible = value;
        }

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
        public StringNumber MaxHeight { get; set; } = 400;

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

        [Obsolete("Use OnOutsideClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> OutsideClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter]
        public bool CloseOnContentClick { get; set; } = true;

        [Obsolete("Use ActivatorContent instead.")]
        [Parameter]
        public RenderFragment<MenuContext> Activator { get; set; }

        [Parameter]
        public RenderFragment<MenuContext> ActivatorContent { get; set; }

        [Parameter]
        public string ActivatorStyle { get; set; }

        [Parameter]
        public bool Block { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        //This just a temp way
        protected virtual IThemeable Themeable { get; }

        public ElementReference ContentRef { get; set; }

        protected abstract Task Click(MouseEventArgs args);

        //Menu的设计需要改进
        [Parameter]
        public bool Input { get; set; }

        protected override void OnParametersSet()
        {
            if (OutsideClick.HasDelegate)
            {
                OnOutsideClick = OutsideClick;
            }

            if (Activator != null)
            {
                ActivatorContent = Activator;
            }
        }

        protected virtual async Task MouseEnter(MouseEventArgs args)
        {
            if (OpenOnHover && !Visible)
            {
                if (VisibleChanged.HasDelegate)
                    await VisibleChanged.InvokeAsync(true);
                else
                    _visible = true;
            }
            else
            {
                PreventRender();
            }
        }

        protected virtual async Task MouseOut(MouseEventArgs args)
        {
            if (OpenOnHover && Visible)
            {
                if (VisibleChanged.HasDelegate)
                    await VisibleChanged.InvokeAsync(false);
                else
                    _visible = false;
            }
            else
            {
                PreventRender();
            }
        }
    }
}
