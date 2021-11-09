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

        /// <summary>
        /// A temporary drawer sits above its application and uses a scrim (overlay) to darken the background
        /// </summary>
        [Parameter]
        public bool Temporary { get; set; }

        protected bool _isMouseover { get; set; }

        public virtual Task Click(MouseEventArgs e)
        {
            return Task.CompletedTask;
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
