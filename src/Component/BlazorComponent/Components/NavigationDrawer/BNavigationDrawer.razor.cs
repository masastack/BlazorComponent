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
        private bool _value;

        protected bool _valueChangedToTrue;
        protected bool _miniVariant = false;

        private bool IsMobileFromJs { get; set; }

        [Inject]
        private Document Document { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Collapses the drawer to a mini-variant until hovering with the mouse
        /// </summary>
        [Parameter]
        public bool ExpandOnHover { get; set; }

        [Parameter]
        public bool HideOverlay { get; set; }

        [Parameter]
        public RenderFragment<Dictionary<string, object>> ImgContent { get; set; }

        [Parameter]
        public bool MiniVariant
        {
            get => _miniVariant;
            set
            {
                if (value == _miniVariant) return;
                _miniVariant = value;
            }
        }

        [Parameter]
        public bool Permanent { get; set; }

        [Parameter]
        public bool ShowOverlay { get; set; }

        [Parameter]
        public string Src { get; set; }

        [Parameter]
        public bool Stateless { get; set; }

        /// <summary>
        /// A temporary drawer sits above its application and uses a scrim (overlay) to darken the background
        /// </summary>
        [Parameter]
        public bool Temporary { get; set; }

        [Parameter]
        public bool Value
        {
            get => _value;
            set
            {
                if (_value == false && value)
                    _valueChangedToTrue = true;

                _value = value;
                IsActive = value;
            }
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        protected bool IsActive { get; set; }

        protected bool InternalShowOverlay => IsActive && (ShowOverlay || (!HideOverlay && (IsMobile || Temporary)));

        protected bool IsMobile => !Stateless && !Permanent && IsMobileFromJs;

        protected bool IsMouseover { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Permanent)
            {
                _ = UpdateValue(true);
            }
            else if (Stateless)
            {
            }
            else if (!Temporary)
            {
                // logic move to firstRender because need to get isMobile by js runtime.
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

                IsMobileFromJs = await JsInvokeAsync<bool>(JsInteropConstants.IsMobile);

                if (!Permanent && !Stateless && !Temporary)
                {
                    await UpdateValue(!IsMobile);
                }
            }
        }

        public virtual Task Click(MouseEventArgs e)
        {
            return Task.CompletedTask;
        }

        private bool CloseConditional()
        {
            // other conditions are handled in js AddOutsideClickEventListener
            return IsActive;
        }

        public virtual Task MouseEnter(MouseEventArgs e)
        {
            if (ExpandOnHover)
                IsMouseover = true;

            return Task.CompletedTask;
        }

        public virtual Task MouseLeave(MouseEventArgs e)
        {
            if (ExpandOnHover)
                IsMouseover = false;

            return Task.CompletedTask;
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
