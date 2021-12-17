using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OneOf;

namespace BlazorComponent
{
    public abstract partial class BNavigationDrawer : BDomComponentBase
    {
        private CancellationTokenSource _cancellationTokenSource;

        [Parameter]
        public bool ExpandOnHover
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool MiniVariant { get; set; }

        [Parameter]
        public EventCallback<bool> MiniVariantChanged { get; set; }

        [Parameter]
        public bool Permanent { get; set; }

        [Parameter]
        public string Src { get; set; }

        [Parameter]
        public bool Stateless { get; set; }

        [Parameter]
        public string Tag
        {
            get
            {
                return GetValue(App ? "nav" : "aside");
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool Temporary { get; set; }

        [Parameter]
        public bool Value
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public bool App { get; set; }

        [Parameter]
        public bool HideOverlay { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment<Dictionary<string, object>> ImgContent { get; set; }

        [Inject]
        private Document Document { get; set; }

        protected bool IsMouseover
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        //TODO: TouchArea,StackMinZIndex

        protected virtual bool IsMobileBreakpoint { get; }

        protected bool IsActive { get; set; }

        protected bool IsMobile => !Stateless && !Permanent && IsMobileBreakpoint;//TODO: fix mobile

        protected bool ShowOverlay
        {
            get
            {
                return (!HideOverlay && IsActive && (IsMobile || Temporary));
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.AddOutsideClickEventListener,
                    DotNetObjectReference.Create(new Invoker<object>(OutsideClick)),
                    new[] { Document.QuerySelector(Ref).Selector });

                if (!Permanent && !Stateless && !Temporary)
                {
                    await UpdateValue(!IsMobile);
                }
            }
        }

        public virtual Task HandleOnClickAsync(MouseEventArgs e)
        {
            return Task.CompletedTask;
        }

        public virtual async Task HandleOnMouseEnterAsync(MouseEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Delay(150, _cancellationTokenSource.Token);

            if (ExpandOnHover)
            {
                IsMouseover = true;
            }
        }

        public virtual async Task HandleOnMouseLeaveAsync(MouseEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Delay(150, _cancellationTokenSource.Token);

            if (ExpandOnHover)
            {
                IsMouseover = false;
            }
        }

        //TODO ontransitionend事件

        private async Task OutsideClick(object _)
        {
            if (!CloseConditional()) return;

            if (Temporary)
            {
                await UpdateValue(false);
            }
        }

        private bool CloseConditional()
        {
            // other conditions are handled in js AddOutsideClickEventListener
            return IsActive;
        }

        protected async Task UpdateValue(bool value)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            else
            {
                Value = value;
            }
        }
    }
}
