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
        private bool _miniVariant = false;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment<NavigationDrawerImgProps> ImgContent { get; set; }

        [Parameter]
        public OneOf<string, SrcObject> Src { get; set; }

        /// <summary>
        /// Collapses the drawer to a mini-variant until hovering with the mouse
        /// </summary>
        [Parameter]
        public bool ExpandOnHover { get; set; }

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
        public EventCallback<bool> MiniVariantChanged { get; set; }

        protected bool _isMouseover { get; set; }

        protected bool _isMiniVariant => 
            (!ExpandOnHover && MiniVariant) || (ExpandOnHover && !_isMouseover);

        public virtual async Task Click(MouseEventArgs e)
        {
            if (MiniVariant)
            {
                MiniVariant = false;
                await MiniVariantChanged.InvokeAsync(_miniVariant);
            }
        }

        public virtual Task MouseEnter(MouseEventArgs e)
        {
            if (ExpandOnHover)
                _isMouseover = true;

            return Task.CompletedTask;
        }

        public virtual Task MouseLeave(MouseEventArgs e)
        {
            if (ExpandOnHover)
                _isMouseover = false;

            return Task.CompletedTask;
        }

        //TODO ontransitionend事件
    }
}
