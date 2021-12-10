using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;

namespace BlazorComponent
{
    public abstract partial class BNavigationDrawer : BDomComponentBase
    {
        protected bool _miniVariant = false;
        private bool IsMobileFromJs { get; set; }

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

        private bool _value = true;

        [Parameter]
        public bool Value
        {
            get => _value;
            set
            {
                if (value == _value) return;
                _value = value;
            }
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        protected bool InternalShowOverlay => IsActive && (ShowOverlay || (!HideOverlay && (IsMobile || Temporary)));

        public bool IsActive => Value;

        protected bool IsMobile => !Stateless && !Permanent && IsMobileFromJs;

        protected bool IsMouseover { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                IsMobileFromJs = await JsInvokeAsync<bool>(JsInteropConstants.IsMobile);
            }
        }

        public virtual Task Click(MouseEventArgs e)
        {
            return Task.CompletedTask;
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

        protected void UpdateValue(bool value)
        {
            if (ValueChanged.HasDelegate)
            {
                ValueChanged.InvokeAsync(value);
            }
            else
            {
                Value = value;
            }
        }
    }
}